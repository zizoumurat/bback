using Buyersoft.Application.Features.CompanyFeatures.Commands.UpdateCompany;
using Buyersoft.Application.Features.CompanyFeatures.Commands.UpdateCompanyLogo;
using Buyersoft.Application.Features.CompanyFeatures.Queries.GetCurrentCompany;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class CompaniesController : ApiController
{
    private readonly ICompanyService _companyService;
    public CompaniesController(IMediator mediator, ICompanyService companyService) : base(mediator)
    {
        _companyService = companyService;
    }

    [HttpGet("current-company")]
    public async Task<IActionResult> GetById()
    {
        GetCurrentCompanyQuery query = new();
        var result = await _mediator.Send(query);
        return Ok(result.result);
    }

    [HttpGet("company-list")]
    public async Task<IActionResult> CompanyList()
    {
        var list = await _companyService.GetCompanyList();
       
        return Ok(list);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCompany([FromForm] UpdateCompanyDto Company)
    {
        UpdateCompanyCommand request = new(Company);
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPut("FileUpload")]
    public async Task<IActionResult> FileUpload([FromForm] IFormFile File)
    {
        UpdateCompanyLogoCommand request = new(File);
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
