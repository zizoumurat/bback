using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.SupplierActionFeatures.Commands.CreateSupplierAction;
public class CreateSupplierActionCommandHandler : ICommandHandler<CreateSupplierActionCommand, CreateSupplierActionCommandResponse>
{
    private readonly ISupplierActionService _supplierActionService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateSupplierActionCommandHandler(ILocalizationService localizationService, ISupplierActionService supplierActionService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _supplierActionService = supplierActionService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateSupplierActionCommandResponse> Handle(CreateSupplierActionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            int userId = _tokenService.GetUserIdByToken();

            await _transactionManager.BeginTransactionAsync();

            await _supplierActionService.AddAsync(companyId, userId, request.SupplierAction);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("SupplierActionCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
