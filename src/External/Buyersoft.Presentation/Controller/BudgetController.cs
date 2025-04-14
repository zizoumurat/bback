using Buyersoft.Application.Features.BudgetFeatures.Commands.CreateBudget;
using Buyersoft.Application.Features.BudgetFeatures.Commands.DeleteBudget;
using Buyersoft.Application.Features.BudgetFeatures.Commands.UpdateBudget;
using Buyersoft.Application.Features.BudgetFeatures.Queries.GetAllBudgets;
using Buyersoft.Application.Features.BudgetFeatures.Queries.GetAvailableBudgetList;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class BudgetsController : ApiController
{
    public BudgetsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] BudgetFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllBudgetsQuery query = new(filter, pagination);
        GetAllBudgetsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("available-list")]
    public async Task<IActionResult> GetAllAvailable()
    {
        var query = new GetAvailableBudgetListQuery();
        var response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateBudget(BudgetDto Budget)
    {
        CreateBudgetCommand request = new(Budget);
        CreateBudgetCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    [AuthorizeWithBearerPolicy("adminPanel.update")]
    public async Task<IActionResult> UpdateBudget(BudgetDto Budget)
    {
        UpdateBudgetCommand request = new(Budget);
        UpdateBudgetCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AuthorizeWithBearerPolicy("adminPanel.delete")]
    public async Task<IActionResult> DeleteBudget([FromRoute] DeleteBudgetCommand request)
    {
        DeleteBudgetCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
