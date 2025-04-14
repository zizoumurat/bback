using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface ICompanyService
{
    Task<CompanyDetailDto> GetCurrentCompany(int id);
    Task UpdateAsync(int id, UpdateCompanyDto company);
    Task<List<SelectListItemDto>> GetCompanyList();
    Task<PaginatedList<SupplierPortfolioDto>> GetSupplierPortfolio(int companyId, SupplierFilterDto filter, PageRequest pagination);
}
