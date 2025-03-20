using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IBankInfoService
{
    Task AddAsync(int companyId, BankInfoDto entity);

    Task UpdateAsync(int companyId, BankInfoDto entity);

    Task DeleteAsync(int id, int companyId);

    Task<PaginatedList<BankInfoListDto>> GetAllAsync(int companyId, BankInfoFilterDto filter, PageRequest pagination);
}
