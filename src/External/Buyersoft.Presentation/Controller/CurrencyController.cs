using Buyersoft.Application.Features.CurrencyParameterFeatures.Queries.GetCurrencyExchangeRates;
using Buyersoft.Application.Services;
using Buyersoft.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;

[Authorize(AuthenticationSchemes = "Bearer")]
public class CurrencyController : ApiController
{
    private readonly ICurrencyService _currencyService;
    public CurrencyController(IMediator mediator, ICurrencyService currencyService) : base(mediator)
    {
        _currencyService = currencyService;
    }

    [HttpGet("all-currency")]
    public async Task<IActionResult> GetAll()
    {
        var list = await _currencyService.GetAllAsync();

        return Ok(list);
    }

    [HttpGet("exchange-rates/{Id}")]
    public async Task<IActionResult> GetCurrencyExchangeRates(int Id)
    {
        var query = new GetCurrencyExchangeRatesQuery(Id);
        var result = await _mediator.Send(query);

        return Ok(result.result);
    }
}
