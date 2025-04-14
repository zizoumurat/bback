using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.BankInfoFeatures.Commands.UpdateBankInfo;
public class UpdateBankInfoCommandHandler : ICommandHandler<UpdateBankInfoCommand, UpdateBankInfoCommandResponse>
{
    private readonly IBankInfoService _bankInfoService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public UpdateBankInfoCommandHandler(ILocalizationService localizationService, IBankInfoService bankInfoService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _bankInfoService = bankInfoService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<UpdateBankInfoCommandResponse> Handle(UpdateBankInfoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _bankInfoService.UpdateAsync(companyId, request.BankInfo);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("BankInfoUpdated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
