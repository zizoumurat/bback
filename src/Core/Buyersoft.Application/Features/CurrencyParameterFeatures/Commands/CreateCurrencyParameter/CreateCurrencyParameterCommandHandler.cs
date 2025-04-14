using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.CurrencyParameterFeatures.Commands.CreateCurrencyParameter;
public class CreateCurrencyParameterCommandHandler : ICommandHandler<CreateCurrencyParameterCommand, CreateCurrencyParameterCommandResponse>
{
    private readonly ICurrencyParameterService _currencyParameterService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateCurrencyParameterCommandHandler(ILocalizationService localizationService, ICurrencyParameterService currencyParameterService, ITokenService tokenService, IUnitOfWork unitOfWork, IMapper mapper, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _currencyParameterService = currencyParameterService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;

    }

    public async Task<CreateCurrencyParameterCommandResponse> Handle(CreateCurrencyParameterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _currencyParameterService.AddAsync(companyId, request.CurrencyParameter);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("CurrencyParameterCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
