using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IRoleService
{
    Task AddAsync(int companyId, RoleDto entity);

    Task UpdateAsync(int companyId, RoleDto entity);

    Task DeleteAsync(int id, int companyId);

    Task UpdateRolePermissionsAsync(int roleId, List<int> permissionIdList);

    Task<IList<RoleListDto>> GetAllAsync(int companyId);

    Task<PaginatedList<RoleListDto>> GetAllAsync(int companyId, RoleFilterDto filter, PageRequest pagination);
}
