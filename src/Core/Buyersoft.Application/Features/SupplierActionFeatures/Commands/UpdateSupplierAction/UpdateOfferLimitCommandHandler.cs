using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.SupplierActionFeatures.Commands.UpdateSupplierAction;
public class UpdateSupplierActionCommandHandler : ICommandHandler<UpdateSupplierActionCommand, UpdateSupplierActionCommandResponse>
{
    private readonly ISupplierActionService _SupplierActionService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public UpdateSupplierActionCommandHandler(ILocalizationService localizationService, ISupplierActionService SupplierActionService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _SupplierActionService = SupplierActionService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<UpdateSupplierActionCommandResponse> Handle(UpdateSupplierActionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int supplierId = _tokenService.GetSupplierIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _SupplierActionService.UpdateStatusAsync(supplierId, request.SupplierAction);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("SupplierActionUpdated"));
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
