using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface ISupplierService
{
    Task<PaginatedList<SupplierListDto>> GetAllAsync(int companyId, SupplierFilterDto filter, PageRequest pagination);
    Task<List<SupplierDtoForCategory>> GetListForCategory(int companyId, int requestGroupId, int? cityId, int channelType);
    Task<PaginatedList<SupplierPortfolioDto>> GetCompanyPortfolio(int companyId, CompanyFilterDto filter, PageRequest pagination);

    Task CreateSupplier(SupplierCreateDto model);
}
