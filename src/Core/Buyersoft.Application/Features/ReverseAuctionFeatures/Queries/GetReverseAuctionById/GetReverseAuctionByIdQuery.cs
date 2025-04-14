using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.ReverseAuctionFeatures.Queries.GetReverseAuctionById;
public sealed record GetReverseAuctionByIdQuery(int id) : IQuery<GetReverseAuctionByIdResponse>;
