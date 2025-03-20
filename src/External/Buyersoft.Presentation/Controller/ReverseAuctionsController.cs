using Buyersoft.Application.Features.ReverseAuctionFeatures.Commands.CreateReverseAuction;
using Buyersoft.Application.Features.ReverseAuctionFeatures.Commands.StartReverseAuction;
using Buyersoft.Application.Features.ReverseAuctionFeatures.Queries.GetAllReverseAuctions;
using Buyersoft.Application.Features.ReverseAuctionFeatures.Queries.GetReverseAuctionById;
using Buyersoft.Application.Features.TemplateFeatures.Queries.GetTemplateById;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class ReverseAuctionsController : ApiController
{
    private readonly IReverseAuctionService _reverseAuctionService;
    public ReverseAuctionsController(IMediator mediator, IReverseAuctionService reverseAuctionService) : base(mediator)
    {
        _reverseAuctionService = reverseAuctionService;
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> GetAll([FromQuery] ReverseAuctionFilterDto filter, [FromQuery] PageRequest pageRequest)
    {
        GetAllReverseAuctionsQuery query = new(filter, pageRequest);
        GetAllReverseAuctionsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> CreateReverseAuction(AddReverseAuctionDto ReverseAuction)
    {
        CreateReverseAuctionCommand request = new(ReverseAuction);
        CreateReverseAuctionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost("change-statu")]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> Start(StartReverseAuctionCommand request)
    {
        StartReverseAuctionCommandResponse response = await _mediator.Send(request);

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetReverseAuctionByIdQuery query = new(id);
        GetReverseAuctionByIdResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }
}
