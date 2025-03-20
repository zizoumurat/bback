using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.RemoveToShortList;
public class RemoveToShortListCommandHandler : ICommandHandler<RemoveToShortListCommand, RemoveToShortListCommandResponse>
{
    private readonly IOfferService _offerService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;
    
    public RemoveToShortListCommandHandler(ILocalizationService localizationService, IOfferService offerService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _offerService = offerService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<RemoveToShortListCommandResponse> Handle(RemoveToShortListCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _offerService.RemoveToShortList(companyId, request.offerId);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("RemovedToShortList"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
