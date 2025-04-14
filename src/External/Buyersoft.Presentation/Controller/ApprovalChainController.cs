using Buyersoft.Application.Features.ApprovalChainFeatures.Commands.CreateApprovalChain;
using Buyersoft.Application.Features.ApprovalChainFeatures.Commands.DeleteApprovalChain;
using Buyersoft.Application.Features.ApprovalChainFeatures.Commands.UpdateApprovalChain;
using Buyersoft.Application.Features.ApprovalChainFeatures.Queries.GetAllApprovalChains;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class ApprovalChainController : ApiController
{
    public ApprovalChainController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] ApprovalChainFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllApprovalChainsQuery query = new(filter, pagination);
        GetAllApprovalChainsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateApprovalChain(ApprovalChainDto ApprovalChain)
    {
        CreateApprovalChainCommand request = new(ApprovalChain);
        CreateApprovalChainCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    [AuthorizeWithBearerPolicy("adminPanel.update")]
    public async Task<IActionResult> UpdateApprovalChain(ApprovalChainDto ApprovalChain)
    {
        UpdateApprovalChainCommand request = new(ApprovalChain);
        UpdateApprovalChainCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AuthorizeWithBearerPolicy("adminPanel.delete")]
    public async Task<IActionResult> DeleteApprovalChain([FromRoute] DeleteApprovalChainCommand request)
    {
        DeleteApprovalChainCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
