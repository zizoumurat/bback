using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.CurrencyParameterFeatures.Queries.GetCurrencyExchangeRates;

public sealed record GetCurrencyExchangeRatesQueryResponse(IList<ExchangeRateDto> result);
