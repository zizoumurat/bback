using Buyersoft.Application.Features.RequestGroupFeatures.Commands.CreateRequestGroup;
using Buyersoft.Application.Features.RequestGroupFeatures.Queries.GetAllRequestGroups;
using Buyersoft.Domain.Dtos;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

public class RequestGroupController : ApiController
{
    public RequestGroupController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        GetAllRequestGroupsQuery query = new();
        GetAllRequestGroupsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateRequestGroup(RequestGroupDto RequestGroup)
    {
        CreateRequestGroupCommand request = new(RequestGroup);
        CreateRequestGroupCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
