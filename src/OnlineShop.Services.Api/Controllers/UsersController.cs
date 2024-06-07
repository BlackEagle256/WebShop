using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Users.Commands.ChangePassword;
using OnlineShop.Application.Users.Commands.UpdateUser;
using OnlineShop.Application.Users.Queries.GetUserById;
using OnlineShop.Contracts.Users;
using OnlineShop.Domain.Abstractions.Maybe;
using OnlineShop.Domain.Abstractions.Result;
using OnlineShop.Domain.Errors;
using OnlineShop.Services.Api.Contracts;
using OnlineShop.Services.Api.Infrastructure;

namespace OnlineShop.Services.Api.Controllers;
public sealed class UsersController : ApiController
{
    public UsersController(IMediator mediator)
        : base(mediator)
    {
    }

    [HttpGet(ApiRoutes.Users.GetById)]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid userId) =>
        await Maybe<GetUserByIdQuery>
            .From(new GetUserByIdQuery(userId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);

    [HttpPut(ApiRoutes.Users.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid userId, UpdateUserRequest updateUserRequest) =>
        await Result.Create(updateUserRequest, DomainErrors.General.UnProcessableRequest)
            .Ensure(request => request.UserId == userId, DomainErrors.General.UnProcessableRequest)
            .Map(request => new UpdateUserCommand(request.UserId, request.FirstName, updateUserRequest.LastName))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HttpPut(ApiRoutes.Users.ChangePassword)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(Guid userId, ChangePasswordRequest changePasswordRequest) =>
        await Result.Create(changePasswordRequest, DomainErrors.General.UnProcessableRequest)
            .Ensure(request => request.UserId == userId, DomainErrors.General.UnProcessableRequest)
            .Map(request => new ChangePasswordCommand(request.UserId, request.Password))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
}