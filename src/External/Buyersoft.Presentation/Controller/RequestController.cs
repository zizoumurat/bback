using Buyersoft.Application.Features.OfferLimitFeatures.Commands.StartApprovalProcess;
using Buyersoft.Application.Features.RequestFeatures.Commands.ApproveReject;
using Buyersoft.Application.Features.RequestFeatures.Commands.AssignManager;
using Buyersoft.Application.Features.RequestFeatures.Commands.CancelBidCollection;
using Buyersoft.Application.Features.RequestFeatures.Commands.CreateComprasionTable;
using Buyersoft.Application.Features.RequestFeatures.Commands.CreateRequest;
using Buyersoft.Application.Features.RequestFeatures.Commands.CreateReverseAuction;
using Buyersoft.Application.Features.RequestFeatures.Commands.DeleteRequest;
using Buyersoft.Application.Features.RequestFeatures.Commands.StartBidCollection;
using Buyersoft.Application.Features.RequestFeatures.Commands.UpdateRequest;
using Buyersoft.Application.Features.RequestFeatures.Queries.GetAllRequestById;
using Buyersoft.Application.Features.RequestFeatures.Queries.GetAllRequests;
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
public class RequestController : ApiController
{
    private readonly IRequestService _requestService;
    public RequestController(IMediator mediator, IRequestService requestService) : base(mediator)
    {
        _requestService = requestService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] RequestFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllRequestsQuery query = new(filter, pagination);
        GetAllRequestsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetAllRequestByIdQuery query = new(id);
        GetAllRequestByIdQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRequest(CreateRequestDto Request)
    {
        CreateRequestCommand request = new(Request);
        CreateRequestCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost("start")]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> StartBidCollection(StartBidCollectionDto Model)
    {
        StartBidCollectionCommand request = new(Model);
        StartBidCollectionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost("assign-manager")]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> AssignManager(AssignManagerDto Model)
    {
        AssignManagerCommand request = new(Model);
        AssignManagerCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost("start-approval-process")]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> StartApprovalProcess(StartApprovalProcessDto Model)
    {
        StartApprovalProcessCommand request = new(Model);
        StartApprovalProcessCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost("approve-reject-request")]
    public async Task<IActionResult> ApproveRejectRequest(ApproveRejectRequestDto Model)
    {
        ApproveRejectCommand request = new(Model);
        ApproveRejectCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost("create-reverse-auction")]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> CreateReverseAuction(AddReverseAuctionDto Model)
    {
        CreateReverseAuctionCommand request = new(Model);
        CreateReverseAuctionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut("create-comprasion-table")]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> CreateComprasionTable(CreateComprasionTableCommand request)
    {
        CreateComprasionTableCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut("cancel")]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> CancelBidCollection(CancelBidCollectionDto Model)
    {
        CancelBidCollectionCommand request = new(Model);
        CancelBidCollectionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }


    [HttpPut]
    [AuthorizeWithBearerPolicy("requests.owner")]
    public async Task<IActionResult> UpdateRequest(CreateRequestDto Request)
    {
        UpdateRequestCommand request = new(Request);
        UpdateRequestCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRequest([FromRoute] DeleteRequestCommand request)
    {
        DeleteRequestCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
