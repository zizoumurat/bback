using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Services;
public interface ISupplierActionService
{
    Task AddAsync(int companyId, int userId, SupplierActionCreateDto entity);
    Task<IList<SupplierActionListDto>> GetListAsync(int companyId, int supplierId);
    Task UpdateStatusAsync(int supplierId, SupplierActionUpdateStatusDto entity);
}
