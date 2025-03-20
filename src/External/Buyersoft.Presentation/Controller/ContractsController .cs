using Buyersoft.Application.Features.BrancheFeatures.Queries.GetAllContracts;
using Buyersoft.Application.Features.BrancheFeatures.Queries.GetComments;
using Buyersoft.Application.Features.ContractFeatures.Commands.ApproveReject;
using Buyersoft.Application.Features.ContractFeatures.Commands.UploadContractFile;
using Buyersoft.Application.Features.RequestFeatures.Commands.ApproveReject;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class ContractsController : ApiController
{
    private readonly INotificationService _notificationService;
    private readonly ITokenService _tokenService;

    public ContractsController(IMediator mediator, ITokenService tokenService, INotificationService noticationService) : base(mediator)
    {
        _tokenService = tokenService;
        _notificationService = noticationService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ContractFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllContractsQuery query = new(filter, pagination);
        GetAllContractsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }


    [HttpGet("comments/{contractId}")]
    public async Task<IActionResult> GetComments(int contractId)
    {
        GetCommentsQuery query = new(contractId);
        GetCommentsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPut("upload-contract-file")]
    public async Task<IActionResult> UploadContractFile([FromForm] UploadContractFileDto Model)
    {
        UploadContractFileCommand command = new(Model);
        UploadContractFileCommandResponse response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpPut("add-comment")]
    public async Task<IActionResult> AddComment([FromBody] AddCommentDto Model)
    {
        AddCommentCommand command = new(Model);
        AddCommentCommandResponse response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpPut("approve-reject-contract")]
    public async Task<IActionResult> ApproveRejectContract(ApproveRejectContractDto Model)
    {
        ApproveRejectContractCommand request = new(Model);
        ApproveRejectContractCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
