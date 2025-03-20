using Buyersoft.Application.Features.ServiceDefinitionFeatures.Commands.CreateServiceDefinition;
using Buyersoft.Application.Features.ServiceDefinitionFeatures.Commands.DeleteServiceDefinition;
using Buyersoft.Application.Features.ServiceDefinitionFeatures.Commands.UpdateServiceDefinition;
using Buyersoft.Application.Features.ServiceDefinitionFeatures.Queries.GetAllServiceDefinitions;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class ServiceDefinitionsController : ApiController
{
    public ServiceDefinitionsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] ServiceDefinitionDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllServiceDefinitionsQuery query = new(filter, pagination);
        GetAllServiceDefinitionsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateServiceDefinition(ServiceDefinitionDto ServiceDefinition)
    {
        CreateServiceDefinitionCommand request = new(ServiceDefinition);
        CreateServiceDefinitionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    [AuthorizeWithBearerPolicy("adminPanel.update")]
    public async Task<IActionResult> UpdateServiceDefinition(ServiceDefinitionDto ServiceDefinition)
    {
        UpdateServiceDefinitionCommand request = new(ServiceDefinition);
        UpdateServiceDefinitionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AuthorizeWithBearerPolicy("adminPanel.delete")]
    public async Task<IActionResult> DeleteServiceDefinition(int id)
    {
        DeleteServiceDefinitionCommand request = new(id);
        DeleteServiceDefinitionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
