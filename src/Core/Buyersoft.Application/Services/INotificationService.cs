using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Services;
public interface INotificationService
{
    Task AddAsync(NotificationDto entity);

    Task DeleteAsync(int companyId);

    Task<IList<NotificationDto>> GetAllWithOutPaginationAsync(int userId);
}
