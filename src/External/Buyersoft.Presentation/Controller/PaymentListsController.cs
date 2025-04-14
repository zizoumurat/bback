using Buyersoft.Application.Features.ContractFeatures.Commands.ApproveReject;
using Buyersoft.Application.Features.PaymentListFeatures.Commands.CreatePaymentList;
using Buyersoft.Application.Features.PaymentListFeatures.Queries.GetAllPaymentLists;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class PaymentListsController : ApiController
{
    public PaymentListsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] PaymentListFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllPaymentListsQuery query = new(filter, pagination);
        GetAllPaymentListsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreatePaymentList(PaymentListCreateDto PaymentList)
    {
        CreatePaymentListCommand request = new(PaymentList);
        CreatePaymentListCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut("approve-reject-payment-list")]
    public async Task<IActionResult> ApproveRejectPaymentList(ApproveRejectPaymentListDto Model)
    {
        ApproveRejectCommand request = new(Model);
        ApproveRejectCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

}
