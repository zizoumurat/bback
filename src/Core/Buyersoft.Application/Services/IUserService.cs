using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IUserService
{
    Task AddAsync(int companyId, UserCreateDto model);
    Task UpdateAsync(int companyId, UserUpdateDto model);
    Task DeleteAsync(int id, int companyId);
    Task UpdateProfileAsync(int userId, UpdateProfileDto model);
    Task ChangePasswordAsync(int userId, UpdatePasswordDto model);


    Task<PaginatedList<UserListDto>> GetPaginationListAsync(int companyId, UserFilterDto filter, PageRequest pagination);
    Task<IList<UserListDto>> GetAllAsync(int companyId, UserFilterDto filter);
    Task<IList<SelectListItemDto>> GetSelectListItemAsync(int companyId);
    Task<IList<SelectListItemDto>> GetOwnerUsers(int companyId);

    Task<UserDetailDto> GetCurrentUser(int userId);
}
