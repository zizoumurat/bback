using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.RoleFeatures.Commands.CreateRole;
public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, CreateRoleCommandResponse>
{
    private readonly IRoleService _roleService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateRoleCommandHandler(ILocalizationService localizationService, IRoleService roleService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _roleService = roleService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _roleService.AddAsync(companyId, request.Role);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("RoleCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
