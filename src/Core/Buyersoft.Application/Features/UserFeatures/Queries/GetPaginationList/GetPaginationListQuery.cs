using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.UserFeatures.Queries.GetPaginationList;
public sealed record GetPaginationListQuery(UserFilterDto filter, PageRequest pagination) : IQuery<GetPaginationListQueryResponse>;
