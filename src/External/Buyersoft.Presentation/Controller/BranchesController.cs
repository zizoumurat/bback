using Buyersoft.Application.Features.BrancheFeatures.Queries.GetAllBranches;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class BranchesController : ApiController
{
    public BranchesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BranchFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllBranchesQuery query = new(filter, pagination);
        GetAllBranchesQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }
}
