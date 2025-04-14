using Buyersoft.Application.Features.SubCategoryFeatures.Commands.CreateSubCategory;
using Buyersoft.Application.Features.SubCategoryFeatures.Queries.GetAllSubCategories;
using Buyersoft.Domain.Dtos;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class SubCategoryController : ApiController
{
    public SubCategoryController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] int mainCategoryId)
    {
        GetAllSubCategoriesQuery query = new(mainCategoryId);
        GetAllSubCategoriesQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateSubCategory(SubCategoryDto SubCategory)
    {
        CreateSubCategoryCommand request = new(SubCategory);
        CreateSubCategoryCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
