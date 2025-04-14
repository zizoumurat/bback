using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.CurrencyParameterFeatures.Queries.GetAllCurrencyParameters;

public sealed record GetAllCurrencyParametersQueryResponse(PaginatedList<CurrencyParameterListDto> result);
