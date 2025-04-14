using Buyersoft.Application.Features.CategoryFeatures.Queries.GetAllCategories;
using Buyersoft.Application.Features.CompanyFeatures.Commands.UpdateCompany;
using Buyersoft.Application.Features.CompanyFeatures.Commands.UpdateCompanyLogo;
using Buyersoft.Application.Features.CompanyFeatures.Queries.GetCurrentCompany;
using Buyersoft.Application.Features.CompanyFeatures.Queries.GetSupplierList;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class CompanySupplierPortfolioController : ApiController
{
    public CompanySupplierPortfolioController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] SupplierFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetSupplierListQuery query = new(filter, pagination);
        GetSupplierListQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }
}
