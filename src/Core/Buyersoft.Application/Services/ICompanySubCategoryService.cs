using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Services;
public interface ICompanySubCategoryService
{
    Task AddAsync(int companyId, CompanySubCategoryDto entity);

    Task<IList<CompanySubCategoryListDto>> GetAllAsync(int companyId, int mainCategoryId);
}
