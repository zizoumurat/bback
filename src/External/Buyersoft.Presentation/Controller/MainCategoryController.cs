using Buyersoft.Application.Features.MainCategoryFeatures.Commands.CreateMainCategory;
using Buyersoft.Application.Features.MainCategoryFeatures.Commands.DeleteMainCategory;
using Buyersoft.Application.Features.MainCategoryFeatures.Commands.UpdateMainCategory;
using Buyersoft.Application.Features.MainCategoryFeatures.Queries.GetAllMainCategories;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class MainCategoryController : ApiController
{
    public MainCategoryController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] MainCategoryFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllMainCategoriesQuery query = new(filter, pagination);
        GetAllMainCategoriesQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateMainCategory(MainCategoryDto MainCategory)
    {
        CreateMainCategoryCommand request = new(MainCategory);
        CreateMainCategoryCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    [AuthorizeWithBearerPolicy("adminPanel.update")]
    public async Task<IActionResult> UpdateMainCategory(MainCategoryDto MainCategory)
    {
        UpdateMainCategoryCommand request = new(MainCategory);
        UpdateMainCategoryCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AuthorizeWithBearerPolicy("adminPanel.delete")]
    public async Task<IActionResult> DeleteMainCategory(DeleteMainCategoryCommand request)
    {
        DeleteMainCategoryCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
