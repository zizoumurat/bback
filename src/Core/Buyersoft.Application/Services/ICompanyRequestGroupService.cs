using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Services;
public interface ICompanyRequestGroupService
{
    Task AddAsync(int companyId, CompanyRequestGroupDto entity);

    Task<IList<CompanyRequestGroupListDto>> GetAllAsync(int companyId, int subCategoryId);
}
