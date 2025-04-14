using AutoMapper;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.RolePermissionRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Buyersoft.Persistance.Services;
public class RoleService : IRoleService
{
    private readonly RoleManager<Role> _roleManager;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;
    private readonly IAddRolePermissionRepository _addRolePermissionRepository;
    private readonly IUpdateRolePermissionRepository _updateRolePermissionRepository;
    private readonly IDeleteRolePermissionRepository _deleteRolePermissionRepository;
    private readonly IQueryRolePermissionRepository _queryRolePermissionRepository;

    public RoleService(IMapper mapper, RoleManager<Role> roleManager, ILocalizationService localizationService, IAddRolePermissionRepository addRolePermissionRepository, IUpdateRolePermissionRepository updateRolePermissionRepository, IDeleteRolePermissionRepository deleteRolePermissionRepository, IQueryRolePermissionRepository queryRolePermissionRepository)
    {
        _mapper = mapper;
        _roleManager = roleManager;
        _localizationService = localizationService;
        _addRolePermissionRepository = addRolePermissionRepository;
        _updateRolePermissionRepository = updateRolePermissionRepository;
        _deleteRolePermissionRepository = deleteRolePermissionRepository;
        _queryRolePermissionRepository = queryRolePermissionRepository;
    }

    public async Task AddAsync(int companyId, RoleDto entity)
    {

        var roleExist = await _roleManager.RoleExistsAsync(entity.Name);

        if (roleExist)
        {
            throw new InvalidOperationException("DuplicateEntity");
        }

        await _roleManager.CreateAsync(new Role()
        {
            Name = entity.Name,
            CompanyId = companyId,
            IsSystemRole = false
        });

    }

    public async Task UpdateAsync(int id, RoleDto Role)
    {
        var entity = await _roleManager.FindByIdAsync(Role.Id.ToString());

        if (entity == null || entity.CompanyId != id)
        {
            throw new InvalidOperationException("EntityNotFound");
        }

        if (entity.IsSystemRole)
        {
            throw new InvalidOperationException("SystemRoleCanNotChange");
        }

        entity.Name = Role.Name;

        await _roleManager.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id, int companyId)
    {
        var roleExist = await _roleManager.FindByIdAsync(id.ToString());

        if (roleExist == null || roleExist.CompanyId != companyId)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        if (roleExist.IsSystemRole)
        {
            throw new InvalidOperationException("SystemRoleCanNotChange");
        }

        await _roleManager.DeleteAsync(roleExist);
    }

    public async Task<PaginatedList<RoleListDto>> GetAllAsync(int companyId, RoleFilterDto filter, PageRequest pagination)
    {
        filter = filter ?? new RoleFilterDto("");

        pagination ??= new PageRequest();
        var query = _roleManager.Roles.Where(x =>
                    (string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name))
                    && (x.CompanyId == null || x.CompanyId == companyId)).AsQueryable();

        var count = query.Count();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Select(x => new RoleListDto()
            {
                Id = x.Id,
                Name = x.Name,
                IsSystemRole = x.IsSystemRole
            }).ToListAsync();


        return new PaginatedList<RoleListDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<IList<RoleListDto>> GetAllAsync(int companyId)
    {
        return await _roleManager.Roles.Where(x => !x.IsHiddenRole && (x.CompanyId == null || x.CompanyId == companyId)).Select(x => new RoleListDto()
        {
            Id = x.Id,
            Name = x.Name,
            IsSystemRole = x.IsSystemRole
        }).ToListAsync();
    }

    public async Task UpdateRolePermissionsAsync(int roleId, List<int> permissionIdList)
    {
        var existingPermissions = await _queryRolePermissionRepository.GetList(x => x.RoleId == roleId).ToListAsync();

        var permissionsToAdd = permissionIdList
            .Where(permissionId => !existingPermissions.Select(x => x.PermissionId).Contains(permissionId))
            .Select(permissionId => new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            }).ToList();

        if (permissionsToAdd.Count > 0)
        {
            await _addRolePermissionRepository.AddRangeAsync(permissionsToAdd);
        }

        var permissionsToRemove = existingPermissions
            .Where(existingPermission => !permissionIdList.Contains(existingPermission.PermissionId))
            .Select(existingPermission => new RolePermission
            {
                Id = existingPermission.Id,
                RoleId = existingPermission.RoleId,
                PermissionId = existingPermission.PermissionId
            }).ToList();

        if (permissionsToRemove.Count > 0)
        {
            _deleteRolePermissionRepository.RemoveRange(permissionsToRemove);
        }
    }
}
