using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.RoleFeatures.Queries.GetAllRoles;
public sealed record GetAllRolesQuery(RoleFilterDto filter, PageRequest pagination) : IQuery<GetAllRolesQueryResponse>;
