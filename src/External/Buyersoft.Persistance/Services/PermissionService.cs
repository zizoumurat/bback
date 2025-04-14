using AutoMapper;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Enums;
using Buyersoft.Domain.Repositories.PermissionRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class PermissionService : IPermissionService
{
    private readonly IAddPermissionRepository _addPermissionRepository;
    private readonly IUpdatePermissionRepository _updatePermissionRepository;
    private readonly IDeletePermissionRepository _deletePermissionRepository;
    private readonly IQueryPermissionRepository _queryPermissionRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public PermissionService(IAddPermissionRepository addPermissionRepository,
        IUpdatePermissionRepository updatePermissionRepository,
        IDeletePermissionRepository deletePermissionRepository,
        IQueryPermissionRepository queryPermissionRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addPermissionRepository = addPermissionRepository;
        _updatePermissionRepository = updatePermissionRepository;
        _deletePermissionRepository = deletePermissionRepository;
        _queryPermissionRepository = queryPermissionRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<IList<PermissionListDto>> GetAllAsync(int companyId)
    {
        var list = await _queryPermissionRepository.GetList(x => true).ToListAsync();

        var result = list
            .Select(p => new
            {
                Module = p.Name.Split('.')[0],
                Action = new PermissionDto
                {
                    Id = p.Id,
                    Name = p.Name.Split('.')[1],
                }
            })
            .GroupBy(p => p.Module)
            .Select(g => new PermissionListDto
            {
                Name = g.Key,
                ActionList = g.Select(x => x.Action).ToList()
            })
            .ToList();

        return result;
    }

    public async Task<IList<PermissionDto>> GetPermissionsByRoleIdAsync(int roleId)
    {
        var rolePermissions = await _queryPermissionRepository
            .GetList(x => x.RolePermissions.Any(rp => rp.RoleId == roleId))
            .SelectMany(x => x.RolePermissions.Where(rp => rp.RoleId == roleId))
            .Select(x => new PermissionDto()
            {
                Id = x.Permission.Id,
                Name = x.Permission.Name,
            }).ToListAsync();

        return rolePermissions;
    }
}
