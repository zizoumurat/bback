using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.CurrencyParameterFeatures.Commands.UpdateCurrencyParameter;
public class UpdateCurrencyParameterCommandHandler : ICommandHandler<UpdateCurrencyParameterCommand, UpdateCurrencyParameterCommandResponse>
{
    private readonly ICurrencyParameterService _currencyParameterService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public UpdateCurrencyParameterCommandHandler(ILocalizationService localizationService, ICurrencyParameterService currencyParameterService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _currencyParameterService = currencyParameterService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<UpdateCurrencyParameterCommandResponse> Handle(UpdateCurrencyParameterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _currencyParameterService.UpdateAsync(companyId, request.CurrencyParameter);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("CurrencyParameterUpdated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
