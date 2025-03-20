using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.RequestFeatures.Queries.GetAllRequests;
public sealed record GetAllRequestsQuery(RequestFilterDto filter, PageRequest pagination) : IQuery<GetAllRequestsQueryResponse>;
