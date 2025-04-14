using Buyersoft.Application.Features.DepartmentFeatures.Commands.CreateDepartment;
using Buyersoft.Application.Features.DepartmentFeatures.Commands.DeleteDepartment;
using Buyersoft.Application.Features.DepartmentFeatures.Commands.UpdateDepartment;
using Buyersoft.Application.Features.DepartmentFeatures.Queries.GetAllDepartments;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class DepartmentsController : ApiController
{
    public DepartmentsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] DepartmentFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllDepartmentsQuery query = new(filter, pagination);
        GetAllDepartmentsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateDepartment(DepartmentDto department)
    {
        CreateDepartmentCommand request = new(department);
        CreateDepartmentCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    [AuthorizeWithBearerPolicy("adminPanel.update")]
    public async Task<IActionResult> UpdateDepartment(DepartmentDto department)
    {
        UpdateDepartmentCommand request = new(department);
        UpdateDepartmentCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AuthorizeWithBearerPolicy("adminPanel.delete")]
    public async Task<IActionResult> DeleteDepartment([FromRoute] DeleteDepartmentCommand request)
    {
        DeleteDepartmentCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
