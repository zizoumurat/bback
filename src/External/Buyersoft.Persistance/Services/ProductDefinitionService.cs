using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.ProductDefinitionRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class ProductDefinitionService : IProductDefinitionService
{
    private readonly IAddProductDefinitionRepository _addProductDefinitionRepository;
    private readonly IUpdateProductDefinitionRepository _updateProductDefinitionRepository;
    private readonly IDeleteProductDefinitionRepository _deleteProductDefinitionRepository;
    private readonly IQueryProductDefinitionRepository _queryProductDefinitionRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public ProductDefinitionService(IAddProductDefinitionRepository addProductDefinitionRepository,
        IUpdateProductDefinitionRepository updateProductDefinitionRepository,
        IDeleteProductDefinitionRepository deleteProductDefinitionRepository,
        IQueryProductDefinitionRepository queryProductDefinitionRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addProductDefinitionRepository = addProductDefinitionRepository;
        _updateProductDefinitionRepository = updateProductDefinitionRepository;
        _deleteProductDefinitionRepository = deleteProductDefinitionRepository;
        _queryProductDefinitionRepository = queryProductDefinitionRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductDefinitionDto>> GetAllAsync(int companyId, ProductDefinitionDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();
        var query = _queryProductDefinitionRepository.GetList(x => x.Category.CompanyId == companyId);

        if (filter != null)
            query = query.Where(x => x.CategoryId == filter.CategoryId);

        query = query.AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<ProductDefinitionDto>(_mapper.ConfigurationProvider)
        .ToListAsync();


        return new PaginatedList<ProductDefinitionDto>(items, count, pagination.Page, pagination.PageSize);
    }
    public async Task AddAsync(int companyId, ProductDefinitionDto entity)
    {
        bool exists = await _queryProductDefinitionRepository.IsExisting(x => x.Code == entity.Code && x.Category.CompanyId == companyId);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<ProductDefinition>(entity);

        await _addProductDefinitionRepository.AddAsync(addEntity);
    }

    public async Task UpdateAsync(int companyId, ProductDefinitionDto entity)
    {
        bool exists = await _queryProductDefinitionRepository.IsExisting(x => x.Code == entity.Code && x.Category.CompanyId == companyId && x.Id != entity.Id);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        exists = await _queryProductDefinitionRepository.IsExisting(x => x.Category.CompanyId == companyId && x.Id == entity.Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var updateEntity = _mapper.Map<ProductDefinition>(entity);

        _updateProductDefinitionRepository.Update(updateEntity);
    }

    public async Task DeleteAsync(int id, int companyId)
    {
        bool exists = await _queryProductDefinitionRepository.IsExisting(x => x.Id == id && x.Category.CompanyId == companyId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        _deleteProductDefinitionRepository.RemoveById(id);
    }

    public async Task<IList<ProductDefinitionDto>> GetAllWithOutPaginationAsync(int companyId)
    {
        var list = await _queryProductDefinitionRepository.GetList(x => x.Category.CompanyId == companyId).ToListAsync();

        return _mapper.Map<List<ProductDefinitionDto>>(list);
    }
}
