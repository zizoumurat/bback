using Buyersoft.Application.Features.CategoryFeatures.Commands.CreateCategory;
using Buyersoft.Application.Features.CategoryFeatures.Commands.DeleteCategory;
using Buyersoft.Application.Features.CategoryFeatures.Commands.ImportCategoryExcel;
using Buyersoft.Application.Features.CategoryFeatures.Commands.UpdateCategory;
using Buyersoft.Application.Features.CategoryFeatures.Queries.ExportExcellCategories;
using Buyersoft.Application.Features.CategoryFeatures.Queries.GetAllCategories;
using Buyersoft.Application.Features.CategoryFeatures.Queries.GetCategoryIdByParameters;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class CategoriesController : ApiController
{
    public CategoriesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] CategoryFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllCategoriesQuery query = new(filter, pagination);
        GetAllCategoriesQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("excell-export")]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> ExportExcell([FromQuery] CategoryFilterDto filter)
    {
        ExportExcellCategoriesQuery query = new(filter);
        ExportExcellCategoriesQueryResponse response = await _mediator.Send(query);

        return File(response.result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "categories.xlsx");
    }

    [HttpGet("find-category")]
    public async Task<IActionResult> FindCategory([FromQuery] CategoryGroupFilter filter)
    {
        GetCategoryIdByParametersQuery query = new(filter);
        GetCategoryIdByParametersQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDto Category)
    {
        CreateCategoryCommand request = new(Category);
        CreateCategoryCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut("excel-import")]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> ExcelImport([FromForm] ImportCategoryExcelCommand command)
    {
        ImportCategoryExcelCommandResponse response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpPut]
    [AuthorizeWithBearerPolicy("adminPanel.update")]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto Category)
    {
        UpdateCategoryCommand request = new(Category);
        UpdateCategoryCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AuthorizeWithBearerPolicy("adminPanel.delete")]
    public async Task<IActionResult> DeleteCategory([FromRoute] DeleteCategoryCommand request)
    {
        DeleteCategoryCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
