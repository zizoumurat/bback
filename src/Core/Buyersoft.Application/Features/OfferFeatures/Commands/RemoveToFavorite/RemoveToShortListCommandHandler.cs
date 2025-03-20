using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.RemoveToFavorite;
public class RemoveToFavoriteCommandHandler : ICommandHandler<RemoveToFavoriteCommand, RemoveToFavoriteCommandResponse>
{
    private readonly IOfferService _offerService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;
    
    public RemoveToFavoriteCommandHandler(ILocalizationService localizationService, IOfferService offerService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _offerService = offerService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<RemoveToFavoriteCommandResponse> Handle(RemoveToFavoriteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _offerService.RemoveToFavorite(companyId, request.offerId);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("RemovedToFavorite"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
