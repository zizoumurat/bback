using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface ITemplateService
{
    Task AddAsync(int companyId, TemplateDto entity);

    Task UpdateAsync(int companyId, TemplateDto entity);

    Task DeleteAsync(int id, int companyId);

    Task<PaginatedList<TemplateListDto>> GetAllAsync(int companyId, TemplateFilterDto filter, PageRequest pagination);
    Task<IList<TemplateListDto>> GetAllByRequestGroupAsync(int requestGroupId);
    Task<TemplateListDto> GetById(int Id);

}
