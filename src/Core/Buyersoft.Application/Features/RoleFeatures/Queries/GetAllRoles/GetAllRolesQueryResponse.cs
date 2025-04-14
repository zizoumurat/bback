using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RoleFeatures.Queries.GetAllRoles;

public sealed record GetAllRolesQueryResponse(PaginatedList<RoleListDto> result);
 