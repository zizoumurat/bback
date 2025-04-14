using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface ISystemNotificationService
{
    Task AddAsync(SystemNotificationDto entity);

    Task UpdateAsync(SystemNotificationDto entity);

    Task DeleteAsync(int id);

    Task<PaginatedList<SystemNotificationListDto>> GetAllAsync(SystemNotificationFilterDto filter, PageRequest pagination);
}
