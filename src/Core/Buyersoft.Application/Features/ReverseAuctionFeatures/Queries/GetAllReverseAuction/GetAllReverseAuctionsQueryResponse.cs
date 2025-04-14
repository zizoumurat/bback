using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.ReverseAuctionFeatures.Queries.GetAllReverseAuctions;

public sealed record GetAllReverseAuctionsQueryResponse(PaginatedList<ReverseAuctionListDto> result);
