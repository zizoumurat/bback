using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.CurrencyParameterFeatures.Queries.GetCurrencyExchangeRates;
public sealed record GetCurrencyExchangeRatesQuery(int Id) : IQuery<GetCurrencyExchangeRatesQueryResponse>;
