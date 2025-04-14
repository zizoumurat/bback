using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.CurrencyParameterRepositories;
using Buyersoft.Domain.Repositories.DepartmentRepositories;
using Buyersoft.Domain.Repositories.SupplierPortfolioRepositories;
using Buyersoft.Domain.Repositories.SupplierRepositories;
using Buyersoft.Domain.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class SupplierService : ISupplierService
{
    private readonly IQuerySupplierRepository _querySupplierRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;
    private readonly IAddCompanyRepository _addCompanyRepository;
    private readonly IAddSupplierRepository _addSupplierRepository;
    private readonly UserManager<User> _userManager;
    private readonly IAddDepartmentRepository _departmentRepository;
    private readonly ITransactionManager _transactionManager;
    private readonly IQueryCompanySupplierPortfolioRepository _queryCompanySupplierPortfolioRepository;


    public SupplierService(IQuerySupplierRepository querySupplierRepository,
        ILocalizationService localizationService,
        IMapper mapper,
        IAddCompanyRepository addCompanyRepository,
        IAddSupplierRepository addSupplierRepository,
        UserManager<User> userManager,
        IDepartmentService departmentService,
        IAddDepartmentRepository departmentRepository,
        ITransactionManager transactionManager,
        IQueryCompanySupplierPortfolioRepository queryCompanySupplierPortfolioRepository)
    {
        _querySupplierRepository = querySupplierRepository;
        _localizationService = localizationService;
        _mapper = mapper;
        _addCompanyRepository = addCompanyRepository;
        _addSupplierRepository = addSupplierRepository;
        _userManager = userManager;
        _departmentRepository = departmentRepository;
        _transactionManager = transactionManager;
        _queryCompanySupplierPortfolioRepository = queryCompanySupplierPortfolioRepository;
    }

    public async Task CreateSupplier(SupplierCreateDto model)
    {
        var company2 = _mapper.Map<Company>(model);

        await _transactionManager.BeginTransactionAsync();

        var company = new Company()
        {
            Name = model.Name,
            ContactPhoneNumber = model.Phone,
            ContactFirstName = model.ContactFirstName,
            ContactLastName = model.ContactLastName,
            Email = model.Email,
            Address = model.Address,
            Website = model.WebSite,
            Phone = model.Phone,
            CityId = model.CityId,
            DistrictId = model.DistrictId,
            TaxAdministration = model.TaxAdministration,
            TaxNumber = model.TaxNumber,
            IsSupplier = true,
            IsDeleted = false,


        };

        await _addCompanyRepository.AddAsync(company);


        var supplier = new Supplier()
        {
            CompanyId = company.Id,
            SupplierCode = "Code"
        };

        supplier.SupplierRequestGroups = model.RequestGroupIdList.Select(x => new SupplierRequestGroup()
        {
            RequestGroupId = x
        }).ToList();

        await _addSupplierRepository.AddAsync(supplier);

        var department = new Department()
        {
            CompanyId = company.Id,
            Name = "Yönetim",
        };

        await _departmentRepository.AddAsync(department);

        string baseUserName = ReplaceTurkishCharacters(string.Join("", company.Name.Split(" ")).ToLower() + "." + model.ContactFirstName.ToLower());
        string userName = baseUserName + "." + Guid.NewGuid().ToString("N").Substring(0, 8);

        var user = new User()
        {
            Name = model.ContactFirstName,
            Surname = model.ContactLastName,
            Email = model.Email,
            RoleId = 3,
            DepartmentId = department.Id,
            PhoneNumber = model.Phone,
            CompanyId = company.Id,
            UserName = userName
        };

        var results = await _userManager.CreateAsync(user, model.Password);

        await _transactionManager.CommitAsync();

        if (!results.Succeeded)
        {
            throw new InvalidOperationException("UserCreateError");
        }

    }

    private string ReplaceTurkishCharacters(string input)
    {
        return input
            .Replace("ç", "c")
            .Replace("ğ", "g")
            .Replace("ü", "u")
            .Replace("ş", "s")
            .Replace("ö", "o")
            .Replace("ı", "i");
    }

    public async Task<PaginatedList<SupplierListDto>> GetAllAsync(int companyId, SupplierFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();
        var query = _querySupplierRepository
            .GetList(x => (filter == null || string.IsNullOrEmpty(filter.Code) &&
                (filter == null || string.IsNullOrEmpty(filter.ErpCode))))
                .AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<SupplierListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return new PaginatedList<SupplierListDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<List<SupplierDtoForCategory>> GetListForCategory(int companyId, int requestGroupId, int? cityId, int channelType)
    {
        return await _querySupplierRepository.GetListForCategory(companyId, requestGroupId, cityId, channelType);
    }

    public async Task<PaginatedList<SupplierPortfolioDto>> GetCompanyPortfolio(int companyId, CompanyFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var supplier = await _querySupplierRepository.GetFirstAsync(x => x.Company.Id == companyId).FirstAsync();

        if(supplier == null)
        {
            throw new InvalidOperationException("SupplierNotFound");
        }

        var query = _queryCompanySupplierPortfolioRepository.GetFirstAsync(x => x.SupplierId == supplier.Id)
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
                Id = x.Company.Id,
                Code = "",
                Name = x.Company.Name,
                City = x.Company.City.Name,
                District = x.Company.District.Name,
                TaxAdministration = x.Company.TaxAdministration,
                Contact = x.Company.ContactFirstName + " " + x.Supplier.Company.ContactLastName
            }).ToListAsync();

        return new PaginatedList<SupplierPortfolioDto>(result, count, pagination.Page, pagination.PageSize);
    }
}
