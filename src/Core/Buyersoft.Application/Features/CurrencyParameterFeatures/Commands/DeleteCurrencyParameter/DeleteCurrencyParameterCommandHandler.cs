using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.CurrencyParameterFeatures.Commands.DeleteCurrencyParameter;

public class DeleteCurrencyParameterCommandHandler : ICommandHandler<DeleteCurrencyParameterCommand, DeleteCurrencyParameterCommandResponse>
{
    private readonly ICurrencyParameterService _currencyParameterService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public DeleteCurrencyParameterCommandHandler(ILocalizationService localizationService, ICurrencyParameterService currencyParameterService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _currencyParameterService = currencyParameterService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<DeleteCurrencyParameterCommandResponse> Handle(DeleteCurrencyParameterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();

            await _transactionManager.BeginTransactionAsync();

            await _currencyParameterService.DeleteAsync(request.Id, companyId);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("CurrencyParameterDeleted"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
