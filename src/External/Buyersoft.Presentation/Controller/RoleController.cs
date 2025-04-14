using Buyersoft.Application.Features.RoleFeatures.Commands.CreateRole;
using Buyersoft.Application.Features.RoleFeatures.Commands.DeleteRole;
using Buyersoft.Application.Features.RoleFeatures.Commands.RolePermission;
using Buyersoft.Application.Features.RoleFeatures.Commands.UpdateRole;
using Buyersoft.Application.Features.RoleFeatures.Queries.GetAllRoles;
using Buyersoft.Application.Features.RoleFeatures.Queries.GetCompleteRoles;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class RolesController : ApiController
{
    private readonly IConfiguration _configuration;
    private readonly IPermissionService _permissionService;

    public RolesController(IMediator mediator, IConfiguration configuration, IPermissionService permissionService) : base(mediator)
    {
        _configuration = configuration;
        _permissionService = permissionService;
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] RoleFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllRolesQuery query = new(filter, pagination);
        GetAllRolesQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("get-complate-role")]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll()
    {
        GetCompleteRolesQuery query = new();
        GetCompleteRolesQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("get-permissions")]
    public async Task<IActionResult> GetPermissions()
    {
        var permissions = await _permissionService.GetAllAsync(1);


        return Ok(permissions);
    }

    [HttpGet("get-permissions-by-role/{id}")]
    public async Task<IActionResult> GetPermissionsByRole(int id)
    {
        var permissions = await _permissionService.GetPermissionsByRoleIdAsync(id);


        return Ok(permissions);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateRole([FromBody] RoleDto Role)
    {
        CreateRoleCommand request = new(Role);
        CreateRoleCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    [AuthorizeWithBearerPolicy("adminPanel.update")]
    public async Task<IActionResult> UpdateRole(RoleDto Role)
    {
        UpdateRoleCommand request = new(Role);
        UpdateRoleCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AuthorizeWithBearerPolicy("adminPanel.delete")]
    public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommand request)
    {
        DeleteRoleCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut("update-permissions")]
    [AuthorizeWithBearerPolicy("adminPanel.update")]
    public async Task<IActionResult> UpdatePermissions([FromBody] UpdateRolePermissionDto RolePermissions)
    {
        RolePermissionCommand request = new(RolePermissions);
        RolePermissionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}