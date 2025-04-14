using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.ReverseAuctionFeatures.Queries.GetAllReverseAuctions;
public sealed record GetAllReverseAuctionsQuery(ReverseAuctionFilterDto filter, PageRequest pagination) : IQuery<GetAllReverseAuctionsQueryResponse>;
