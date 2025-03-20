using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.RequestFeatures.Queries.GetAllOffer;
public sealed record GetAllOfferQuery(RequestFilterDto filter, PageRequest pagination) : IQuery<GetAllOfferQueryResponse>;
