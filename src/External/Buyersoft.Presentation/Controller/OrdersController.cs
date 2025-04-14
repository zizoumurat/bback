using Buyersoft.Application.Features.DepartmentFeatures.Commands.CreateDepartment;
using Buyersoft.Application.Features.DepartmentFeatures.Commands.DeleteDepartment;
using Buyersoft.Application.Features.DepartmentFeatures.Commands.UpdateDepartment;
using Buyersoft.Application.Features.DepartmentFeatures.Queries.GetAllDepartments;
using Buyersoft.Application.Features.OfferLimitFeatures.Commands.MakeOffer;
using Buyersoft.Application.Features.OrderFeatures.Commands.CancelOrder;
using Buyersoft.Application.Features.OrderFeatures.Commands.ChangeOrderStatus;
using Buyersoft.Application.Features.OrderFeatures.Commands.DeliveredOrder;
using Buyersoft.Application.Features.OrderFeatures.Commands.SetNonconformity;
using Buyersoft.Application.Features.OrderFeatures.Queries.GetAllOrder;
using Buyersoft.Application.Features.RequestFeatures.Queries.GetAllOffer;
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
public class OrdersController : ApiController
{
    private readonly IOrderService _OrderService;
    public OrdersController(IMediator mediator, IOrderService OrderService) : base(mediator)
    {
        _OrderService = OrderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] OrderPreparationFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllOrderQuery query = new(filter, pagination);
        GetAllOrderQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost("set-nonconformity")]
    public async Task<IActionResult> SetNonconformity([FromBody] SetNonconformityDto Model)
    {
        SetNonconformityCommand request = new(Model);
        SetNonconformityCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost("cancel-order")]
    public async Task<IActionResult> CancelOrder([FromBody] CancelOrderDto Model)
    {
        CancelOrderCommand request = new(Model);
        CancelOrderCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost("delivered-order")]
    public async Task<IActionResult> DeliveredOrder([FromBody] DeliveredOrderDto Model)
    {
        DeliveredOrderCommand request = new(Model);
        DeliveredOrderCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost("change-status")]
    public async Task<IActionResult> ChangeOrderStatus([FromBody] ChangeOrderStatusDto Model)
    {
        ChangeOrderStatusCommand request = new(Model);
        ChangeOrderStatusCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
