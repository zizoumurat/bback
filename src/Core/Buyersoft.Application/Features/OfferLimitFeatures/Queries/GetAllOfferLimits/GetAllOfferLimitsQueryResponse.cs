using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Queries.GetAllOfferLimits;

public sealed record GetAllOfferLimitsQueryResponse(PaginatedList<OfferLimitListDto> result);
