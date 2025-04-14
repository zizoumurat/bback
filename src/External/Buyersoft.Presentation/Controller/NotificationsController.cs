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
public class NotificationsController : ApiController
{
    private readonly INotificationService _notificationService;
    private readonly ITokenService _tokenService;

    public NotificationsController(IMediator mediator, ITokenService tokenService, INotificationService noticationService) : base(mediator)
    {
        _tokenService = tokenService;
        _notificationService = noticationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] LocationFilterDto filter, [FromQuery] PageRequest pageRequest)
    {
        var userId = _tokenService.GetUserIdByToken();
        var list = await _notificationService.GetAllWithOutPaginationAsync(userId);

        return Ok(list);
    }
}
