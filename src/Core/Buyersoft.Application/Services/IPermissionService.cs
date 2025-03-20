using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Services;
public interface IPermissionService
{
    Task<IList<PermissionListDto>> GetAllAsync(int companyId);
    Task<IList<PermissionDto>> GetPermissionsByRoleIdAsync(int roleId);

}
