using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Services;
public interface ISubCategoryService
{
    Task AddAsync(int companyId, SubCategoryDto entity);

    Task<IList<SubCategoryListDto>> GetAllAsync(int companyId, int mainCategoryId);
}
