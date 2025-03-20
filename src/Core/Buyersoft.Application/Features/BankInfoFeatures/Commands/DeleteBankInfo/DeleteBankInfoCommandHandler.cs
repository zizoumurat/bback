using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.BankInfoFeatures.Commands.DeleteBankInfo;

public class DeleteBankInfoCommandHandler : ICommandHandler<DeleteBankInfoCommand, DeleteBankInfoCommandResponse>
{
    private readonly IBankInfoService _bankInfoService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public DeleteBankInfoCommandHandler(ILocalizationService localizationService, IBankInfoService bankInfoService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _bankInfoService = bankInfoService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<DeleteBankInfoCommandResponse> Handle(DeleteBankInfoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _bankInfoService.DeleteAsync(request.Id, companyId);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("BankInfoDeleted"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
