using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Services;
public interface IRequestGroupService
{
    Task AddAsync(int companyId, RequestGroupDto entity);

    Task<IList<RequestGroupListDto>> GetAllAsync(int subCategoryId);
}
