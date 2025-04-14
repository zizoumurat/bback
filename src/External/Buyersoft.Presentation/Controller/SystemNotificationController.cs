using Buyersoft.Application.Features.SystemNotificationFeatures.Commands.CreateSystemNotification;
using Buyersoft.Application.Features.SystemNotificationFeatures.Commands.DeleteSystemNotification;
using Buyersoft.Application.Features.SystemNotificationFeatures.Commands.UpdateSystemNotification;
using Buyersoft.Application.Features.SystemNotificationFeatures.Queries.GetAllSystemNotifications;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class SystemNotificationController : ApiController
{
    public SystemNotificationController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] SystemNotificationFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllSystemNotificationsQuery query = new(filter, pagination);
        GetAllSystemNotificationsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateSystemNotification(SystemNotificationDto SystemNotification)
    {
        CreateSystemNotificationCommand request = new(SystemNotification);
        CreateSystemNotificationCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    [AuthorizeWithBearerPolicy("adminPanel.update")]
    public async Task<IActionResult> UpdateSystemNotification(SystemNotificationDto SystemNotification)
    {
        UpdateSystemNotificationCommand request = new(SystemNotification);
        UpdateSystemNotificationCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AuthorizeWithBearerPolicy("adminPanel.delete")]
    public async Task<IActionResult> DeleteSystemNotification(DeleteSystemNotificationCommand request)
    {
        DeleteSystemNotificationCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
