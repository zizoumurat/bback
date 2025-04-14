using Buyersoft.Application.Features.CompanyCompanySubCategoryFeatures.Commands.CreateCompanySubCategory;
using Buyersoft.Application.Features.CompanySubCategoryFeatures.Commands.CreateCompanySubCategory;
using Buyersoft.Application.Features.CompanySubCategoryFeatures.Queries.GetAllCompanySubCategories;
using Buyersoft.Domain.Dtos;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class CompanySubCategoriesController : ApiController
{
    public CompanySubCategoriesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int mainCategoryId)
    {
        GetAllCompanySubCategoriesQuery query = new(mainCategoryId);
        GetAllCompanySubCategoriesQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateSubCategory(CompanySubCategoryDto SubCategory)
    {
        CreateCompanySubCategoryCommand request = new(SubCategory);
        CreateCompanySubCategoryCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
