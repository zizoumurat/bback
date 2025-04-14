using Buyersoft.Application.Features.DepartmentFeatures.Commands.CreateDepartment;
using Buyersoft.Application.Features.DepartmentFeatures.Commands.DeleteDepartment;
using Buyersoft.Application.Features.DepartmentFeatures.Commands.UpdateDepartment;
using Buyersoft.Application.Features.DepartmentFeatures.Queries.GetAllDepartments;
using Buyersoft.Application.Features.OrderPreparationFeatures.Commands.CreateOrder;
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
public class OrderPreparationsController : ApiController
{
    private readonly IOrderPreparationService _orderPreparationService;
    public OrderPreparationsController(IMediator mediator, IOrderPreparationService orderPreparationService) : base(mediator)
    {
        _orderPreparationService = orderPreparationService;
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] OrderPreparationFilterDto filter, [FromQuery] PageRequest pagination)
    {
        var list = await _orderPreparationService.GetAllAsync(1, filter, pagination);

        return Ok(list);
    }

    [HttpPost("create-order")]
    public async Task<IActionResult> CreateOrder(OrderCreateDto Model)
    {
        CreateOrderCommand request = new(Model);
        CreateOrderCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
