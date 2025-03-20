using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RequestFeatures.Queries.GetListByRequestId;

public sealed record GetListByRequestIdQueryResponse(List<OfferListDto> result);
