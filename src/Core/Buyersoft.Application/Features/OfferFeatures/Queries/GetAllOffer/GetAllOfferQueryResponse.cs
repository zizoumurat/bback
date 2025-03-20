using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RequestFeatures.Queries.GetAllOffer;

public sealed record GetAllOfferQueryResponse(PaginatedList<RequestListDto> result);
