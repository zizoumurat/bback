using Buyersoft.Application.Features.OfferLimitFeatures.Commands.CreateOfferLimit;
using Buyersoft.Application.Features.OfferLimitFeatures.Commands.DeleteOfferLimit;
using Buyersoft.Application.Features.OfferLimitFeatures.Commands.UpdateOfferLimit;
using Buyersoft.Application.Features.OfferLimitFeatures.Queries.GetAllOfferLimits;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class OfferLimitController : ApiController
{
    public OfferLimitController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] OfferLimitFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllOfferLimitsQuery query = new(filter, pagination);
        GetAllOfferLimitsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateOfferLimit(OfferLimitDto OfferLimit)
    {
        CreateOfferLimitCommand request = new(OfferLimit);
        CreateOfferLimitCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    [AuthorizeWithBearerPolicy("adminPanel.update")]
    public async Task<IActionResult> UpdateOfferLimit(OfferLimitDto OfferLimit)
    {
        UpdateOfferLimitCommand request = new(OfferLimit);
        UpdateOfferLimitCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AuthorizeWithBearerPolicy("adminPanel.delete")]
    public async Task<IActionResult> DeleteOfferLimit([FromRoute] int id)
    {
        DeleteOfferLimitCommand request = new(id);
        DeleteOfferLimitCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
