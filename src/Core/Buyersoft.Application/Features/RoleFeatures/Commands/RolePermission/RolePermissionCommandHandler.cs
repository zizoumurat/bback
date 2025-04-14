using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.RoleFeatures.Commands.RolePermission;
public class RolePermissionCommandHandler : ICommandHandler<RolePermissionCommand, RolePermissionCommandResponse>
{
    private readonly IRoleService _roleService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public RolePermissionCommandHandler(ILocalizationService localizationService, IRoleService roleService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _roleService = roleService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<RolePermissionCommandResponse> Handle(RolePermissionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _roleService.UpdateRolePermissionsAsync(request.RolePermission.RoleId,
                request.RolePermission.PermissionIdList);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("RoleUpdated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
