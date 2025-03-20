using Buyersoft.Application.Features.SupplierActionFeatures.Commands.CreateSupplierAction;
using Buyersoft.Application.Features.SupplierActionFeatures.Commands.UpdateSupplierAction;
using Buyersoft.Application.Features.SupplierActionFeatures.Queries.GetAllByCompany;
using Buyersoft.Application.Features.SupplierActionFeatures.Queries.GetAllBySupplier;
using Buyersoft.Domain.Dtos;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class SupplierActionsController : ApiController
{
    public SupplierActionsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("get-list-by-company/{SupplierId}")]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetListByCompany([FromRoute] int SupplierId)
    {
        GetAllByCompanyQuery query = new(SupplierId);
        GetAllByCompanyQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("get-list-by-supplier/{CompanyId}")]
    public async Task<IActionResult> GetListBySupplier([FromRoute] int CompanyId)
    {
        GetAllBySupplierQuery query = new(CompanyId);
        GetAllBySupplierQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateSupplierAction(SupplierActionCreateDto SupplierAction)
    {
        CreateSupplierActionCommand request = new(SupplierAction);
        CreateSupplierActionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSupplierAction(SupplierActionUpdateStatusDto SupplierAction)
    {
        UpdateSupplierActionCommand request = new(SupplierAction);
        UpdateSupplierActionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
