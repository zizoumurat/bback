using Buyersoft.Application.Services;
using Buyersoft.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class TaxOfficesController : ApiController
{
    private readonly ITaxOfficeService _taxOfficeService;
    public TaxOfficesController(IMediator mediator, ITaxOfficeService taxOfficeService) : base(mediator)
    {
        _taxOfficeService = taxOfficeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int cityId)
    {
        var list = await _taxOfficeService.GetAllAsync(cityId);

        return Ok(list);
    }
}
