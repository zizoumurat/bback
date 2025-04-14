using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.UserFeatures.Queries.GetPaginationList;

public sealed record GetPaginationListQueryResponse(PaginatedList<UserListDto> result);
