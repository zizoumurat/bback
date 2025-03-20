using Buyersoft.Application.Features.LocationFeatures.Commands.CreateLocation;
using Buyersoft.Application.Features.LocationFeatures.Commands.DeleteLocation;
using Buyersoft.Application.Features.LocationFeatures.Commands.UpdateLocation;
using Buyersoft.Application.Features.LocationFeatures.Queries.GetAllLocations;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class LocationsController : ApiController
{
    private readonly ITokenService _tokenService;
    private readonly ILocationService _locationService;

    public LocationsController(IMediator mediator, ITokenService tokenService, ILocationService locationService) : base(mediator)
    {
        _tokenService = tokenService;
        _locationService = locationService;
    }

    [HttpGet]
    [AuthorizeWithBearerPolicy("adminPanel.read")]
    public async Task<IActionResult> GetAll([FromQuery] LocationFilterDto filter, [FromQuery] PageRequest pageRequest)
    {
        GetAllLocationsQuery query = new(filter, pageRequest);
        GetAllLocationsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    [AuthorizeWithBearerPolicy("adminPanel.create")]
    public async Task<IActionResult> CreateLocation(LocationDto Location)
    {
        CreateLocationCommand request = new(Location);
        CreateLocationCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    [AuthorizeWithBearerPolicy("adminPanel.update")]
    public async Task<IActionResult> UpdateLocation(LocationDto Location)
    {
        UpdateLocationCommand request = new(Location);
        UpdateLocationCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [AuthorizeWithBearerPolicy("adminPanel.delete")]
    public async Task<IActionResult> DeleteLocation([FromRoute] DeleteLocationCommand request)
    {
        DeleteLocationCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpGet("AllList")]
    public async Task<IActionResult> GetAllList()
    {
        var companyId = _tokenService.GetCompanyIdByToken();
        var list = await _locationService.GetAllWithOutPaginationAsync(companyId);

        return Ok(list);
    }
}
