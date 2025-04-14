using Buyersoft.Application.Features.CompanyRequestGroupFeatures.Commands.CreateCompanyRequestGroup;
using Buyersoft.Application.Features.CompanyRequestGroupFeatures.Queries.GetAllCompanyRequestGroups;
using Buyersoft.Domain.Dtos;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class CompanyRequestGroupsController : ApiController
{
    public CompanyRequestGroupsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int subCategoryId)
    {
        GetAllCompanyRequestGroupsQuery query = new(subCategoryId);
        GetAllCompanyRequestGroupsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateCompanyRequestGroup(CompanyRequestGroupDto CompanyRequestGroup)
    {
        CreateCompanyRequestGroupCommand request = new(CompanyRequestGroup);
        CreateCompanyRequestGroupCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}
