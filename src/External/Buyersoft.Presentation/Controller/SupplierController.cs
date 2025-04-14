using Buyersoft.Application.Features.BrancheFeatures.Queries.GetAllSuppliers;
using Buyersoft.Application.Features.BrancheFeatures.Queries.GetListForCategory;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class SupplierController : ApiController
{
    public SupplierController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] SupplierFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllSuppliersQuery query = new(filter, pagination);
        GetAllSuppliersQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("get-list-by-category")]
    public async Task<IActionResult> GetListByCategory([FromQuery] int categoryId, [FromQuery] int? cityId, [FromQuery] int channelType)
    {
        GetListForCategoryQuery query = new(categoryId, cityId, channelType);
        GetListForCategoryQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }
}
