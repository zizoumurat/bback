using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.CategoryRepositories;
using Buyersoft.Domain.Repositories.CategoryUserRepositories;
using Buyersoft.Domain.Repositories.CompanyRequestGroupRepositories;
using Buyersoft.Domain.Repositories.CompanySubCategoryRepositories;
using Buyersoft.Domain.Repositories.LocationRepositories;
using Buyersoft.Domain.Repositories.RequestGroupRepositories;
using Buyersoft.Domain.Repositories.SubCategoryRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace Buyersoft.Persistance.Services;

public class CategoryService : ICategoryService
{
    private readonly IAddCategoryUserRepository _addCategoryUserRepository;
    private readonly IAddCategoryRepository _addCategoryRepository;
    private readonly IUpdateCategoryRepository _updateCategoryRepository;
    private readonly IDeleteCategoryRepository _deleteCategoryRepository;
    private readonly IQueryCategoryRepository _queryCategoryRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IQueryCompanySubCategoryRepository _queryCompanySubCategoryRepository;
    private readonly IQuerySubCategoryRepository _querySubCategoryRepository;
    private readonly IQueryRequestGroupRepository _queryRequestGroupRepository;
    private readonly IQueryCompanyRequestGroupRepository _queryCompanyRequestGroupRepository;
    private readonly IAddCompanySubCategoryRepository _addCompanySubCategoryRepository;
    private readonly IAddCompanyRequestGroupRepository _addCompanyRequestGroupRepository;
    private readonly IQueryLocationRepository _queryLocationRepository;
    private readonly IAddLocationRepository _addLocationRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public CategoryService(IAddCategoryRepository addCategoryRepository,
        IUpdateCategoryRepository updateCategoryRepository,
        IDeleteCategoryRepository deleteCategoryRepository,
        IQueryCategoryRepository queryCategoryRepository,
        ILocalizationService localizationService,
        IMapper mapper,
        IAddCategoryUserRepository addCategoryUserRepository,
        IAddCompanySubCategoryRepository addCompanySubCategoryRepository,
        IQuerySubCategoryRepository querySubCategoryRepository,
        IQueryRequestGroupRepository queryRequestGroupRepository,
        IQueryCompanyRequestGroupRepository queryCompanyRequestGroupRepository,
        IAddCompanyRequestGroupRepository addCompanyRequestGroupRepository,
        IQueryLocationRepository queryLocationRepository,
        IAddLocationRepository addLocationRepository,
        UserManager<User> userManager,
        IQueryCompanySubCategoryRepository queryCompanySubCategoryRepository)
    {
        _addCategoryRepository = addCategoryRepository;
        _updateCategoryRepository = updateCategoryRepository;
        _deleteCategoryRepository = deleteCategoryRepository;
        _queryCategoryRepository = queryCategoryRepository;
        _localizationService = localizationService;
        _mapper = mapper;
        _addCategoryUserRepository = addCategoryUserRepository;
        _addCompanySubCategoryRepository = addCompanySubCategoryRepository;
        _querySubCategoryRepository = querySubCategoryRepository;
        _queryRequestGroupRepository = queryRequestGroupRepository;
        _queryCompanyRequestGroupRepository = queryCompanyRequestGroupRepository;
        _addCompanyRequestGroupRepository = addCompanyRequestGroupRepository;
        _queryLocationRepository = queryLocationRepository;
        _addLocationRepository = addLocationRepository;
        _userManager = userManager;
        _queryCompanySubCategoryRepository = queryCompanySubCategoryRepository;
    }

    public async Task<PaginatedList<CategoryListDto>> GetAllAsync(int companyId, CategoryFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _queryCategoryRepository.GetList(x => x.CompanyId == companyId && !x.IsDeleted);

        if (filter != null)
        {
            query = query.Where(x =>
                (filter.LeadTime == default || x.LeadTime == filter.LeadTime) &&
                (filter.UserId == default || x.CategoryUsers.Any(u => u.UserId == filter.UserId)) &&
                (filter.MainCategoryId == default || x.MainCategoryId == filter.MainCategoryId) &&
                (filter.SubCategoryId == default || x.SubCategoryId == filter.SubCategoryId)
            );
        }


        query = query.AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<CategoryListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return new PaginatedList<CategoryListDto>(items, count, pagination.Page, pagination.PageSize);
    }
    public async Task AddAsync(int companyId, CategoryDto entity)
    {
        bool exists = await _queryCategoryRepository.IsExisting(x => x.MainCategoryId == entity.MainCategoryId && x.CompanyId == companyId && x.SubCategoryId == entity.SubCategoryId && x.RequestGroupId == entity.RequestGroupId);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<Category>(entity);
        addEntity.CompanyId = companyId;

        var users = entity.UserIdList.Select(userId => new CategoryUser()
        {
            UserId = userId,
            CategoryId = addEntity.Id
        }).ToList();

        addEntity.CategoryUsers = users;

        await _addCategoryRepository.AddAsync(addEntity);
    }

    public async Task UpdateAsync(int companyId, CategoryDto entity)
    {
        bool exists = await _queryCategoryRepository.IsExisting(x =>
            x.MainCategoryId == entity.MainCategoryId &&
            x.CompanyId == companyId &&
            x.SubCategoryId == entity.SubCategoryId &&
            x.RequestGroupId == entity.RequestGroupId &&
            x.Id != entity.Id);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        exists = await _queryCategoryRepository.IsExisting(x =>
            x.CompanyId == companyId &&
            x.Id == entity.Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var existEntity = await _queryCategoryRepository.GetFirstAsync(x => x.Id == entity.Id, true).Include(x => x.CategoryUsers).FirstAsync();

        _mapper.Map(entity, existEntity);
        existEntity.CompanyId = companyId;
        existEntity.CategoryUsers.Clear();
        var users = entity.UserIdList.Select(userId => new CategoryUser()
        {
            UserId = userId,
            CategoryId = existEntity.Id
        }).ToList();

        existEntity.CategoryUsers = users;

        _updateCategoryRepository.Update(existEntity);
    }


    public async Task DeleteAsync(int id, int companyId)
    {
        bool exists = await _queryCategoryRepository.IsExisting(x => x.Id == id && x.CompanyId == companyId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        _deleteCategoryRepository.RemoveById(id);
    }

    public async Task<CategoryListDto> GetCategoryIdByParameters(int companyId, CategoryGroupFilter filter)
    {
        var category = await _queryCategoryRepository.GetFirstAsync(x => x.CompanyId == companyId &&
            x.MainCategoryId == filter.mainCategoryId && x.SubCategory.CompanySubCategory.Id == filter.subCategoryId &&
            x.RequestGroup.CompanyRequestGroup.Id == filter.requestGroupId && !x.IsDeleted)
            .Include(x => x.Location)
            .Include(x => x.MainCategory)
            .Include(x => x.SubCategory)
            .Include(x => x.RequestGroup)
            .ProjectTo<CategoryListDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return category;
    }

    public async Task<byte[]> GetExcellData(int companyId, CategoryFilterDto filter)
    {
        var query = _queryCategoryRepository.GetList(x => x.CompanyId == companyId && !x.IsDeleted);

        if (filter != null)
        {
            query = query.Where(x =>
                (filter.LeadTime == default || x.LeadTime == filter.LeadTime) &&
                (filter.UserId == default || x.CategoryUsers.Any(u => u.UserId == filter.UserId)) &&
                (filter.MainCategoryId == default || x.MainCategoryId == filter.MainCategoryId) &&
                (filter.SubCategoryId == default || x.SubCategoryId == filter.SubCategoryId)
            );
        }

        query.Include(query => query.ProductDefinitions).Include(query => query.ServiceDefinitions);
        query = query.AsQueryable();

        var categoryList = await query
            .ProjectTo<CategoryExcellListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var package = new ExcelPackage())
        {
            var categoriesSheet = package.Workbook.Worksheets.Add("Categories");
            AddHeaders(categoriesSheet);


            int categoryRow = 2;
            int definitionRow = 2;

            var definitionsSheet = package.Workbook.Worksheets.Add("Definitions");

            definitionsSheet.Cells[1, 1].Value = "ID";
            definitionsSheet.Cells[1, 2].Value = "Ürün Kodu";
            definitionsSheet.Cells[1, 3].Value = "Ürün / Hizmet Tanımı";
            definitionsSheet.Cells[1, 4].Value = "Kategori ID";

            foreach (var category in categoryList)
            {
                categoriesSheet.Cells[categoryRow, 1].Value = category.Id;
                categoriesSheet.Cells[categoryRow, 2].Value = category.MainCategoryName;
                categoriesSheet.Cells[categoryRow, 3].Value = category.CompanySubCategoryName;
                categoriesSheet.Cells[categoryRow, 4].Value = category.SubCategoryName;
                categoriesSheet.Cells[categoryRow, 5].Value = category.CompanyRequestGroupName;
                categoriesSheet.Cells[categoryRow, 6].Value = category.RequestGroupName;
                categoriesSheet.Cells[categoryRow, 7].Value = category.LocationName;
                categoriesSheet.Cells[categoryRow, 8].Value = string.Join(", ", category.OwnerUserList);
                categoriesSheet.Cells[categoryRow, 9].Value = category.LeadTime;
                categoriesSheet.Cells[categoryRow, 10].Value = category.Unit;
                categoryRow++;

                if (category.MainCategoryId == 1)
                    foreach (var product in category.ProductDefinitions)
                    {
                        definitionsSheet.Cells[definitionRow, 1].Value = product.Id;
                        definitionsSheet.Cells[definitionRow, 2].Value = product.Code;
                        definitionsSheet.Cells[definitionRow, 3].Value = product.Definition;
                        definitionsSheet.Cells[definitionRow, 4].Value = product.CategoryId;
                        definitionRow++;
                    }

                if (category.MainCategoryId == 2)
                    foreach (var product in category.ServiceDefinitionDtos)
                    {
                        definitionsSheet.Cells[definitionRow, 1].Value = product.Id;
                        definitionsSheet.Cells[definitionRow, 2].Value = string.Empty;
                        definitionsSheet.Cells[definitionRow, 3].Value = product.Definition;
                        definitionsSheet.Cells[definitionRow, 4].Value = product.CategoryId;
                        definitionRow++;
                    }
            }


            // Excel dosyasını byte[] olarak döndür
            return package.GetAsByteArray();
        }
    }
    private void AddHeaders(ExcelWorksheet worksheet)
    {
        int columnIndex = 0;
        worksheet.Cells[1, ++columnIndex].Value = "ID";
        worksheet.Cells[1, ++columnIndex].Value = "Ana Kategori";
        worksheet.Cells[1, ++columnIndex].Value = "Alt Kategori 1";
        worksheet.Cells[1, ++columnIndex].Value = "Alt Kategori 2";
        worksheet.Cells[1, ++columnIndex].Value = "Talep Grubu 1";
        worksheet.Cells[1, ++columnIndex].Value = "Talep Grubu 2";
        worksheet.Cells[1, ++columnIndex].Value = "Talep Lokasyonu";
        worksheet.Cells[1, ++columnIndex].Value = "Yetkili Kullanıcı";
        worksheet.Cells[1, ++columnIndex].Value = "Tedarik Süresi";
        worksheet.Cells[1, ++columnIndex].Value = "Birim";
    }

    public async Task ImportExcellAsync(int companyId, IFormFile file)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);

            using (var package = new ExcelPackage(stream))
            {
                // Categories Sheet
                var categoriesSheet = package.Workbook.Worksheets["Categories"];
                if (categoriesSheet != null)
                {
                    var categories = new List<Category>();

                    for (int row = 2; row <= categoriesSheet.Dimension.End.Row; row++)
                    {
                        var categoryId = categoriesSheet.Cells[row, 1].GetValue<string>();
                        var categoryName = categoriesSheet.Cells[row, 2].GetValue<string>();
                        var companySubCategoryName = categoriesSheet.Cells[row, 3].GetValue<string>();
                        var subCategoryName = categoriesSheet.Cells[row, 4].GetValue<string>();
                        var companyRequestGroupName = categoriesSheet.Cells[row, 5].GetValue<string>();
                        var requestGroupName = categoriesSheet.Cells[row, 6].GetValue<string>();
                        var requestLocation = categoriesSheet.Cells[row, 7].GetValue<string>();
                        var ownerUsers = categoriesSheet.Cells[row, 8].GetValue<string>()?.Split(',');
                        var leadTime = categoriesSheet.Cells[row, 9].GetValue<int>();
                        var unit = categoriesSheet.Cells[row, 10].GetValue<string>();

                        if (ownerUsers.Length == 0)
                            throw new InvalidOperationException(_localizationService.GetLocalizedString("OwnerUserNotFound"));


                        var subCategory = await _querySubCategoryRepository.GetFirstAsync(x => x.Name == subCategoryName).FirstAsync() ?? throw new InvalidOperationException(_localizationService.GetLocalizedString("InvalidSubCategory"));
                        var requestGroup = await _queryRequestGroupRepository.GetFirstAsync(x => x.Name == requestGroupName).FirstAsync() ?? throw new InvalidOperationException(_localizationService.GetLocalizedString("InvalidRequestGroup"));

                        var companySubCategory = await _queryCompanySubCategoryRepository.GetFirstAsync(x => x.Name == companySubCategoryName).FirstAsync();

                        if (companySubCategory == null)
                        {
                            companySubCategory = new CompanySubCategory()
                            {
                                Name = companySubCategoryName,
                                SubCategoryId = subCategory.Id,
                                CompanyId = companyId
                            };

                            await _addCompanySubCategoryRepository.AddAsync(companySubCategory);
                        }

                        var companyRequestGroup = await _queryCompanyRequestGroupRepository.GetList(x => x.Name == companyRequestGroupName).FirstOrDefaultAsync();

                        if (companyRequestGroup == null)
                        {
                            companyRequestGroup = new CompanyRequestGroup()
                            {
                                Name = companyRequestGroupName,
                                RequestGroupId = requestGroup.Id,
                                CompanyId = companyId
                            };

                            await _addCompanyRequestGroupRepository.AddAsync(companyRequestGroup);
                        }

                        var location = await _queryLocationRepository.GetFirstAsync(x => x.Name == requestLocation && x.CompanyId == companyId).FirstAsync();

                        if (location == null)
                        {
                            location = new Location()
                            {
                                Name = requestLocation,
                                CompanyId = companyId
                            };

                            await _addCompanyRequestGroupRepository.AddAsync(companyRequestGroup);
                        }


                        if (string.IsNullOrEmpty(categoryId))
                        {
                            var category = new Category()
                            {
                                CompanyId = companyId,
                                MainCategoryId = categoryName.ToLower() == "ürün" ? 1 : 2,
                                SubCategoryId = subCategory.Id,
                                RequestGroupId = requestGroup.Id,
                                LocationId = location.Id,
                                LeadTime = leadTime,
                                Unit = unit,
                                IsDeleted = false
                            };

                            category.CategoryUsers = new List<CategoryUser>();

                            foreach (var owner in ownerUsers)
                            {
                                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Name + " " + x.Surname == owner.Trim());

                                if (user == null)
                                    throw new InvalidOperationException(_localizationService.GetLocalizedString("OwnerUserNotFound"));

                                category.CategoryUsers.Add(new CategoryUser()
                                {
                                    UserId = user.Id,
                                    CategoryId = category.Id
                                });
                            }

                            await _addCategoryRepository.AddAsync(category);
                        }
                    }
                }

                // Definitions Sheet
                var definitionsSheet = package.Workbook.Worksheets["Definitions"];

                if (definitionsSheet != null)
                {

                    for (int row = 2; row <= definitionsSheet.Dimension.End.Row; row++)
                    {
                        var definitionId = definitionsSheet.Cells[row, 1].GetValue<string>();
                        var productCode = definitionsSheet.Cells[row, 2].GetValue<string>();
                        var definition = definitionsSheet.Cells[row, 3].GetValue<string>();
                        var categoryId = definitionsSheet.Cells[row, 4].GetValue<int>();

                        var category = await _queryCategoryRepository.GetFirstAsync(x => x.Id == categoryId, true)
                            .Include(x => x.ProductDefinitions)
                            .Include(x => x.ServiceDefinitions).FirstAsync();

                        if (category.MainCategoryId == 1)
                        {
                            if (string.IsNullOrEmpty(definitionId))
                            {
                                var productDefinition = new ProductDefinition()
                                {
                                    CategoryId = categoryId,
                                    Code = productCode,
                                    Definition = definition
                                };

                                category.ProductDefinitions.Add(productDefinition);
                            }
                            else
                            {
                                var finded = category.ProductDefinitions.First(x => x.Id == Convert.ToInt32(definitionId));
                                finded.Definition = definition;
                                finded.Code = productCode;
                            }
                        }
                        if (category.MainCategoryId == 2)
                        {
                            if (string.IsNullOrEmpty(definitionId))
                            {
                                var serviceDefinition = new ServiceDefinition()
                                {
                                    CategoryId = categoryId,
                                    Definition = definition
                                };

                                category.ServiceDefinitions.Add(serviceDefinition);
                            }
                            else
                            {
                                var finded = category.ServiceDefinitions.First(x => x.Id == Convert.ToInt32(definitionId));
                                finded.Definition = definition;
                            }
                        }

                        _updateCategoryRepository.Update(category);
                    }
                }
            }
        }
    }
}
