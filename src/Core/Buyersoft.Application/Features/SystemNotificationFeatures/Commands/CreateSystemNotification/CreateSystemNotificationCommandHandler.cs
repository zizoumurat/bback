using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.SystemNotificationFeatures.Commands.CreateSystemNotification;
public class CreateSystemNotificationCommandHandler : ICommandHandler<CreateSystemNotificationCommand, CreateSystemNotificationCommandResponse>
{
    private readonly ISystemNotificationService _systemNotificationService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateSystemNotificationCommandHandler(ILocalizationService localizationService, ISystemNotificationService systemNotificationService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _systemNotificationService = systemNotificationService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateSystemNotificationCommandResponse> Handle(CreateSystemNotificationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _systemNotificationService.AddAsync(request.SystemNotification);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("SystemNotificationCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
