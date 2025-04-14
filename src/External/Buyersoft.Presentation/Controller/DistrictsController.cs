using Buyersoft.Application.Services;
using Buyersoft.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class DistrictsController : ApiController
{
    private readonly IDistrictService _districtService;
    public DistrictsController(IMediator mediator, IDistrictService districtService) : base(mediator)
    {
        _districtService = districtService;
    }

    [HttpGet("AllList")]
    public async Task<IActionResult> GetAll([FromQuery] int cityId)
    {
        var list = await _districtService.GetAllAsync(cityId);

        return Ok(list);
    }
}
