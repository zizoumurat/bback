using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Queries.GetAllOfferLimits;
public sealed record GetAllOfferLimitsQuery(OfferLimitFilterDto filter, PageRequest pagination) : IQuery<GetAllOfferLimitsQueryResponse>;
