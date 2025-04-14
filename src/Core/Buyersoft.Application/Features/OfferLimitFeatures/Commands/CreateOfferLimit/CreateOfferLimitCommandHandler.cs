using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.CreateOfferLimit;
public class CreateOfferLimitCommandHandler : ICommandHandler<CreateOfferLimitCommand, CreateOfferLimitCommandResponse>
{
    private readonly IOfferLimitService _offerLimitService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;
    
    public CreateOfferLimitCommandHandler(ILocalizationService localizationService, IOfferLimitService offerLimitService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _offerLimitService = offerLimitService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateOfferLimitCommandResponse> Handle(CreateOfferLimitCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _offerLimitService.AddAsync(companyId, request.OfferLimit);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("OfferLimitCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
