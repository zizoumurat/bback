using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IServiceDefinitionService
{
    Task AddAsync(int companyId, ServiceDefinitionDto entity);

    Task UpdateAsync(int companyId, ServiceDefinitionDto entity);

    Task DeleteAsync(int id, int companyId);
    Task<PaginatedList<ServiceDefinitionDto>> GetAllAsync(int companyId,ServiceDefinitionDto filter, PageRequest pagination);
}
