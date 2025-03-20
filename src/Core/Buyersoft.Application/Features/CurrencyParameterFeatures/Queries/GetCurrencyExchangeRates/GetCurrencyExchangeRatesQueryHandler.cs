using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.CurrencyParameterFeatures.Queries.GetCurrencyExchangeRates;

public sealed class GetCurrencyExchangeRatesQueryHandler : IQueryHandler<GetCurrencyExchangeRatesQuery, GetCurrencyExchangeRatesQueryResponse>
{
    private readonly ICurrencyParameterService _CurrencyParameterService;
    private readonly ITokenService _tokenService;

    public GetCurrencyExchangeRatesQueryHandler(ICurrencyParameterService CurrencyParameterService, ITokenService tokenService)
    {
        _CurrencyParameterService = CurrencyParameterService;
        _tokenService = tokenService;
    }

    public async Task<GetCurrencyExchangeRatesQueryResponse> Handle(GetCurrencyExchangeRatesQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _CurrencyParameterService.GetCurrencyExchangeRates(companyId, request.Id);

        return new(result);
    }
}