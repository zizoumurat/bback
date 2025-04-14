using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IBranchService
{
    Task<PaginatedList<BranchListDto>> GetAllAsync(int companyId, BranchFilterDto filter, PageRequest pagination);
    Task<IList<SelectListItemDto>> GetSelectListItemsAsync(Dictionary<string, string> Filters);
}
