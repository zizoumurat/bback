using Buyersoft.Application.Features.CurrencyParameterFeatures.Commands.CreateCurrencyParameter;
using Buyersoft.Application.Features.CurrencyParameterFeatures.Commands.DeleteCurrencyParameter;
using Buyersoft.Application.Features.CurrencyParameterFeatures.Commands.UpdateCurrencyParameter;
using Buyersoft.Application.Features.CurrencyParameterFeatures.Queries.GetAllCurrencyParameters;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class CurrencyParametersController : ApiController
{
    public CurrencyParametersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] CurrencyParameterFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllCurrencyParametersQuery query = new(filter, pagination);
        GetAllCurrencyParametersQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateCurrencyParameter(CurrencyParameterDto CurrencyParameter)
    {
        CreateCurrencyParameterCommand request = new(CurrencyParameter);
        CreateCurrencyParameterCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    [AuthorizeWithBearerPolicy("adminPanel.update")]
    public async Task<IActionResult> UpdateCurrencyParameter(CurrencyParameterDto CurrencyParameter)
    {
        UpdateCurrencyParameterCommand request = new(CurrencyParameter);
        UpdateCurrencyParameterCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AuthorizeWithBearerPolicy("adminPanel.delete")]
    public async Task<IActionResult> DeleteCurrencyParameter(int id)
    {
        DeleteCurrencyParameterCommand request = new(id);
        DeleteCurrencyParameterCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
