using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.BankInfoFeatures.Commands.CreateBankInfo;
public class CreateBankInfoCommandHandler : ICommandHandler<CreateBankInfoCommand, CreateBankInfoCommandResponse>
{
    private readonly IBankInfoService _bankInfoService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateBankInfoCommandHandler(ILocalizationService localizationService, IBankInfoService bankInfoService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _bankInfoService = bankInfoService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateBankInfoCommandResponse> Handle(CreateBankInfoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _bankInfoService.AddAsync(companyId, request.BankInfo);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("BankInfoCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
