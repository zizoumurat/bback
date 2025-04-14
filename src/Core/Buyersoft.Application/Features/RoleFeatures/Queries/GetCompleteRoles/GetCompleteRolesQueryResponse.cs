using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RoleFeatures.Queries.GetCompleteRoles;

public sealed record GetCompleteRolesQueryResponse(IList<RoleListDto> result);
 