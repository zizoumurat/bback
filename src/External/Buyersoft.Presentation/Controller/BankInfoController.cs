using Buyersoft.Application.Features.BankInfoFeatures.Commands.CreateBankInfo;
using Buyersoft.Application.Features.BankInfoFeatures.Commands.DeleteBankInfo;
using Buyersoft.Application.Features.BankInfoFeatures.Commands.UpdateBankInfo;
using Buyersoft.Application.Features.BankInfoFeatures.Queries.GetAllBankInfos;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class BankInfoController : ApiController
{
    public BankInfoController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] BankInfoFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllBankInfosQuery query = new(filter, pagination);
        GetAllBankInfosQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateBankInfo(BankInfoDto Model)
    {
        CreateBankInfoCommand request = new(Model);
        CreateBankInfoCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    [AuthorizeWithBearerPolicy("adminPanel.update")]
    public async Task<IActionResult> UpdateBankInfo(BankInfoDto Model)
    {
        UpdateBankInfoCommand request = new(Model);
        UpdateBankInfoCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AuthorizeWithBearerPolicy("adminPanel.delete")]
    public async Task<IActionResult> DeleteBankInfo([FromRoute] DeleteBankInfoCommand request)
    {
        DeleteBankInfoCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
