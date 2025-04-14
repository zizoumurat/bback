using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.UpdateOfferLimit;
public class UpdateOfferLimitCommandHandler : ICommandHandler<UpdateOfferLimitCommand, UpdateOfferLimitCommandResponse>
{
    private readonly IOfferLimitService _offerLimitService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public UpdateOfferLimitCommandHandler(ILocalizationService localizationService, IOfferLimitService offerLimitService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _offerLimitService = offerLimitService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<UpdateOfferLimitCommandResponse> Handle(UpdateOfferLimitCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _offerLimitService.UpdateAsync(companyId, request.OfferLimit);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("OfferLimitUpdated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
