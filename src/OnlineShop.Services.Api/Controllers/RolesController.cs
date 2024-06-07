using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Roles.Commands.CreateRole;
using OnlineShop.Application.Roles.Commands.UpdateRole;
using OnlineShop.Application.Roles.Queries.GetRoleByName;
using OnlineShop.Contracts.Roles;
using OnlineShop.Domain.Abstractions.Maybe;
using OnlineShop.Domain.Abstractions.Result;
using OnlineShop.Domain.Errors;
using OnlineShop.Services.Api.Contracts;
using OnlineShop.Services.Api.Infrastructure;

namespace OnlineShop.Services.Api.Controllers;

public class RolesController : ApiController
{
    public RolesController(IMediator mediator)
        : base(mediator)
    {
    }

    [HttpPost(ApiRoutes.Roles.create)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateRoleRequest createRoleRequest) =>
        await Result.Create(createRoleRequest, DomainErrors.General.UnProcessableRequest)
            .Map(request => new CreateRoleCommand(request.Name, request.Description))
            .Bind(request => Mediator.Send(request))
            .Match(Ok, BadRequest);
    
    
    [HttpPut(ApiRoutes.Roles.update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(UpdateRoleRequest updateRoleRequest) =>
        await Result.Create(updateRoleRequest, DomainErrors.General.UnProcessableRequest)
            .Map(request => new UpdateRoleCommand(request.Id,request.Name, request.Description))
            .Bind(request => Mediator.Send(request))
            .Match(Ok, BadRequest);


    [HttpGet(ApiRoutes.Roles.GetByName)]
    [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(string roleName) =>
        await Maybe<GetRoleByNameQuery>
            .From(new GetRoleByNameQuery(roleName))
            .Bind(request => Mediator.Send(request))
            .Match(Ok, BadRequest);
}
