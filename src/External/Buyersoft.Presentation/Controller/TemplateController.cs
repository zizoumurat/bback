using Buyersoft.Application.Features.RequestFeatures.Queries.GetAllRequestById;
using Buyersoft.Application.Features.TemplateFeatures.Commands.CreateTemplate;
using Buyersoft.Application.Features.TemplateFeatures.Commands.DeleteTemplate;
using Buyersoft.Application.Features.TemplateFeatures.Commands.UpdateTemplate;
using Buyersoft.Application.Features.TemplateFeatures.Queries.GetAllByRequestGroup;
using Buyersoft.Application.Features.TemplateFeatures.Queries.GetAllTemplates;
using Buyersoft.Application.Features.TemplateFeatures.Queries.GetTemplateById;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class TemplateController : ApiController
{
    public TemplateController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] TemplateFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllTemplatesQuery query = new(filter, pagination);
        GetAllTemplatesQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTemplate(TemplateDto Template)
    {
        CreateTemplateCommand request = new(Template);
        CreateTemplateCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTemplate(TemplateDto Template)
    {
        UpdateTemplateCommand request = new(Template);
        UpdateTemplateCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AuthorizeWithBearerPolicy("requests.creator")]
    public async Task<IActionResult> DeleteTemplate(DeleteTemplateCommand request)
    {
        DeleteTemplateCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetTemplateByIdQuery query = new(id);
        GetTemplateByIdResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("get-by-request-group/{requestGroupId}")]
    public async Task<IActionResult> GetByRequestGroup([FromRoute] int requestGroupId)
    {
        GetAllByRequestGroupQuery query = new(requestGroupId);
        GetAllByRequestGroupResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }
}
