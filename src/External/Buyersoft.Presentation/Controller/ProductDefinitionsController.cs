using Buyersoft.Application.Features.ProductDefinitionFeatures.Commands.CreateProductDefinition;
using Buyersoft.Application.Features.ProductDefinitionFeatures.Commands.DeleteProductDefinition;
using Buyersoft.Application.Features.ProductDefinitionFeatures.Commands.UpdateProductDefinition;
using Buyersoft.Application.Features.ProductDefinitionFeatures.Queries.GetAllProductDefinitions;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class ProductDefinitionsController : ApiController
{
    public ProductDefinitionsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ProductDefinitionDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllProductDefinitionsQuery query = new(filter, pagination);
        GetAllProductDefinitionsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateProductDefinition(ProductDefinitionDto ProductDefinition)
    {
        CreateProductDefinitionCommand request = new(ProductDefinition);
        CreateProductDefinitionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    [AuthorizeWithBearerPolicy("adminPanel.update")]
    public async Task<IActionResult> UpdateProductDefinition(ProductDefinitionDto ProductDefinition)
    {
        UpdateProductDefinitionCommand request = new(ProductDefinition);
        UpdateProductDefinitionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AuthorizeWithBearerPolicy("adminPanel.delete")]
    public async Task<IActionResult> DeleteProductDefinition(int id)
    {
        DeleteProductDefinitionCommand request = new(id);
        DeleteProductDefinitionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
