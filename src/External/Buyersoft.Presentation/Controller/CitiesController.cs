using Buyersoft.Application.Services;
using Buyersoft.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class CitiesController : ApiController
{
    private readonly ICityService _cityService;
    public CitiesController(IMediator mediator, ICityService cityService) : base(mediator)
    {
        _cityService = cityService;
    }

    [HttpGet("AllList")]
    public async Task<IActionResult> GetAll()
    {
        var list = await _cityService.GetAllAsync();

        return Ok(list);
    }
}
