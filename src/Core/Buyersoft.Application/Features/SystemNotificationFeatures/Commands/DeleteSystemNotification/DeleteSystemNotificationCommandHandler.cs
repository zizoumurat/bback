using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.SystemNotificationFeatures.Commands.DeleteSystemNotification;

public class DeleteSystemNotificationCommandHandler : ICommandHandler<DeleteSystemNotificationCommand, DeleteSystemNotificationCommandResponse>
{
    private readonly ISystemNotificationService _systemNotificationService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public DeleteSystemNotificationCommandHandler(ILocalizationService localizationService, ISystemNotificationService systemNotificationService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _systemNotificationService = systemNotificationService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<DeleteSystemNotificationCommandResponse> Handle(DeleteSystemNotificationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _systemNotificationService.DeleteAsync(request.Id);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("SystemNotificationDeleted"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
