using AutoMapper;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.CompanyRepositories;
using Buyersoft.Domain.Repositories.SupplierPortfolioRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class CompanyService : ICompanyService
{
    private readonly IQueryCompanyRepository _queryCompanyRepository;
    private readonly IQueryCompanySupplierPortfolioRepository _queryCompanySupplierPortfolioRepository;
    private readonly IUpdateCompanyRepository _updateCompanyRepository;
    private readonly IDocumentService _documentService;
    private readonly IMapper _mapper;

    public CompanyService(IQueryCompanyRepository queryCompanyRepository, IMapper mapper, IUpdateCompanyRepository updateCompanyRepository, IDocumentService documentService, IQueryCompanySupplierPortfolioRepository queryCompanySupplierPortfolioRepository)
    {
        _queryCompanyRepository = queryCompanyRepository;
        _mapper = mapper;
        _updateCompanyRepository = updateCompanyRepository;
        _documentService = documentService;
        _queryCompanySupplierPortfolioRepository = queryCompanySupplierPortfolioRepository;
    }

    public async Task<List<SelectListItemDto>> GetCompanyList()
    {
        var result = await _queryCompanyRepository.GetList(x => x.IsSupplier == false && x.IsDeleted == false)
            .Select(x => new SelectListItemDto(x.Id, x.Name)).ToListAsync();

        return result;
    }

    public async Task<CompanyDetailDto> GetCurrentCompany(int id)
    {
        var company = await _queryCompanyRepository.GetFirstAsync(x => x.Id == id).Include(x => x.Logo).FirstAsync();

        return _mapper.Map<CompanyDetailDto>(company);
    }

    public async Task<PaginatedList<SupplierPortfolioDto>> GetSupplierPortfolio(int companyId, SupplierFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _queryCompanySupplierPortfolioRepository.GetFirstAsync(x => x.CompanyId == companyId)
            .Include(x => x.Supplier)
            .ThenInclude(s => s.Company)
            .ThenInclude(c => c.City)
            .Include(x => x.Supplier.Company.District);

        var count = await query.CountAsync();

        var result = await query
             .Skip((pagination.Page - 1) * pagination.PageSize)
              .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Select(x => new SupplierPortfolioDto()
        {
            Id = x.Supplier.Id,
            Code = x.Supplier.SupplierCode,
            Name = x.Supplier.Company.Name,
            City = x.Supplier.Company.City.Name,
            District = x.Supplier.Company.District.Name,
            TaxAdministration = x.Supplier.Company.TaxAdministration,
            Contact = x.Supplier.Company.ContactFirstName + " " + x.Supplier.Company.ContactLastName
        }).ToListAsync();

        return new PaginatedList<SupplierPortfolioDto>(result, count, pagination.Page, pagination.PageSize);
    }

    public async Task UpdateAsync(int id, UpdateCompanyDto company)
    {
        if (id != company.Id)
        {
            throw new InvalidOperationException("EntityNotFound");
        }

        var entity = await _queryCompanyRepository.GetByIdAsync(id);

        if (entity == null)
        {
            throw new InvalidOperationException("EntityNotFound");
        }

        if (company.Logo != null)
        {
            if (entity.LogoId == null)
            {
                int fileId = await _documentService.UploadLogoAsync(company.Logo);
                entity.LogoId = fileId;
            }
            else
            {
                await _documentService.ChangeLogoAsync(company.Logo, entity.LogoId.Value);
            }
        }

        var updateEntity = _mapper.Map<Company>(company);

        updateEntity.IsDeleted = entity.IsDeleted;
        updateEntity.IsSupplier = entity.IsSupplier;
        updateEntity.LogoId = entity.LogoId;

        _updateCompanyRepository.Update(updateEntity);
    }
}
