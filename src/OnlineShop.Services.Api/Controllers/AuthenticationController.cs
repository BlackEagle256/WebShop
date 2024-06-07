using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Authentication.Commands.Login;
using OnlineShop.Application.Users.Commands.CreateUser;
using OnlineShop.Contracts.Authentication;
using OnlineShop.Domain.Abstractions.Maybe;
using OnlineShop.Domain.Abstractions.Result;
using OnlineShop.Domain.Errors;
using OnlineShop.Services.Api.Contracts;
using OnlineShop.Services.Api.Infrastructure;

namespace OnlineShop.Services.Api.Controllers;

[AllowAnonymous]
public sealed class AuthenticationController : ApiController
{
    public AuthenticationController(IMediator mediator)
        : base(mediator)
    {
    }

    [HttpPost(ApiRoutes.Authentication.Login)]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(LoginRequest loginRequest) =>
        await Result.Create(loginRequest, DomainErrors.General.UnProcessableRequest)
            .Map(request => new LoginCommand(request.Email, request.Password))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Authentication.Register)]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(RegisterRequest registerRequest) =>
        await Result.Create(registerRequest, DomainErrors.General.UnProcessableRequest)
            .Map(request => new CreateUserCommand(request.FirstName, request.LastName, request.Email, request.Password))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
}