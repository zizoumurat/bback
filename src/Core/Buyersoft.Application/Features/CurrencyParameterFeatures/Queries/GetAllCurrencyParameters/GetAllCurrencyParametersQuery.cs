using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.CurrencyParameterFeatures.Queries.GetAllCurrencyParameters;
public sealed record GetAllCurrencyParametersQuery(CurrencyParameterFilterDto filter, PageRequest pagination) : IQuery<GetAllCurrencyParametersQueryResponse>;
