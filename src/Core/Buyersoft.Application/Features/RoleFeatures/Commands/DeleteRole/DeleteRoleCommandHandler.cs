using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.RoleFeatures.Commands.DeleteRole;

public class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand, DeleteRoleCommandResponse>
{
    private readonly IRoleService _roleService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public DeleteRoleCommandHandler(ILocalizationService localizationService, IRoleService roleService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _roleService = roleService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _roleService.DeleteAsync(request.Id, companyId);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("RoleDeleted"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
