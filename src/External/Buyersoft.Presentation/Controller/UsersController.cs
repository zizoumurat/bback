using Buyersoft.Application.Features.UserFeatures.Commands.ChangePassword;
using Buyersoft.Application.Features.UserFeatures.Commands.CreateUser;
using Buyersoft.Application.Features.UserFeatures.Commands.DeleteUser;
using Buyersoft.Application.Features.UserFeatures.Commands.UpdateProfile;
using Buyersoft.Application.Features.UserFeatures.Commands.UpdateUser;
using Buyersoft.Application.Features.UserFeatures.Queries.GetPaginationList;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class UsersController : ApiController
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public UsersController(IMediator mediator, IUserService userService, ITokenService tokenService) : base(mediator)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] UserFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetPaginationListQuery query = new(filter, pagination);
        GetPaginationListQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("all-users")]
    public async Task<IActionResult> GetAllList()
    {
        var companyId = _tokenService.GetCompanyIdByToken();
        var list = await _userService.GetAllAsync(companyId, null);

        return Ok(list);
    }
    [HttpGet("owner-user-list")]
    public async Task<IActionResult> GetOwnerUsers()
    {
        var companyId = _tokenService.GetCompanyIdByToken();
        var list = await _userService.GetOwnerUsers(companyId);

        return Ok(list);
    }


    [HttpGet("current-user")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = _tokenService.GetUserIdByToken();
        var user = await _userService.GetCurrentUser(userId);

        return Ok(user);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateUser(UserCreateDto User)
    {
        CreateUserCommand request = new(User);
        CreateUserCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    [AuthorizeWithBearerPolicy("adminPanel.update")]
    public async Task<IActionResult> UpdateUser(UserUpdateDto User)
    {
        UpdateUserCommand request = new(User);
        UpdateUserCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut("update-profile")]
    public async Task<IActionResult> UpdateCurrentUser([FromForm] UpdateProfileDto User)
    {
        UpdateProfileCommand request = new(User);
        UpdateProfileCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromForm] UpdatePasswordDto Model)
    {
        ChangePasswordCommand request = new(Model);
        ChangePasswordCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AuthorizeWithBearerPolicy("adminPanel.delete")]
    public async Task<IActionResult> DeleteUser([FromRoute] DeleteUserCommand request)
    {
        DeleteUserCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
