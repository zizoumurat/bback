using Buyersoft.Application.Features.OfferLimitFeatures.Commands.AddToShortList;
using Buyersoft.Application.Features.OfferLimitFeatures.Commands.MakeOffer;
using Buyersoft.Application.Features.OfferFeatures.Commands.RequestRevision;
using Buyersoft.Application.Features.OfferLimitFeatures.Commands.SetAllocation;
using Buyersoft.Application.Features.RequestFeatures.Queries.GetListByRequestId;
using Buyersoft.Domain.Dtos;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Buyersoft.Application.Features.OfferFeatures.Commands.RejectOffer;
using Buyersoft.Domain.Pagination;
using Buyersoft.Application.Features.RequestFeatures.Queries.GetAllOffer;
using Buyersoft.Application.Features.OfferLimitFeatures.Commands.RemoveToShortList;
using Buyersoft.Application.Features.OfferLimitFeatures.Commands.ChangePrices;
using Buyersoft.Application.Features.OfferLimitFeatures.Commands.AddToFavorite;
using Buyersoft.Application.Features.OfferLimitFeatures.Commands.RemoveToFavorite;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class OffersController : ApiController
{
    public OffersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] RequestFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllOfferQuery query = new(filter, pagination);
        GetAllOfferQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("get-list-by-request/{requestId}")]
    public async Task<IActionResult> GetListByRequest([FromRoute] int requestId)
    {
        GetListByRequestIdQuery request = new(requestId);
        GetListByRequestIdQueryResponse response = await _mediator.Send(request);

        return Ok(response.result);
    }

    [HttpPut("make-offer")]
    [AuthorizeWithBearerPolicy("offers.makeOffer")]
    public async Task<IActionResult> MakeOffer([FromForm] MakeOfferDto MakeOffer)
    {
        MakeOfferCommand request = new(MakeOffer);
        MakeOfferCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost("change-prices")]
    [AuthorizeWithBearerPolicy("offers.makeOffer")]
    public async Task<IActionResult> ChangePrices(List<UpdateOfferPriceDto> Model)
    {
        ChangePricesCommand request = new(Model);
        ChangePricesCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut("create-allocation")]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> CreateAllocation(SetAllocationCommand request)
    {
        SetAllocationCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }


    [HttpPut("add-to-short-list")]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> AddToShortList(AddToShortListCommand request)
    {
        AddToShortListCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut("add-to-favorite")]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> AddToFavorite(AddToFavoriteCommand request)
    {
        AddToFavoriteCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut("remove-to-favorite")]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> RemoveToFavorite(RemoveToFavoriteCommand request)
    {
        RemoveToFavoriteCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut("remove-to-short-list")]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> RemoveToShortList(RemoveToShortListCommand request)
    {
        RemoveToShortListCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost("request-revision")]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> RequestRevision(RequestRevisionCommand request)
    {
        RequestRevisionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost("reject-offer")]
    [AuthorizeWithBearerPolicy("offers.makeOffer")]
    public async Task<IActionResult> RejectOffer(RejectOfferDto Model)
    {
        RejectOfferCommand request = new(Model);
        RejectOfferCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
