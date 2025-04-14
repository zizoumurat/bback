using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.MakeOffer;
public class MakeOfferCommandHandler : ICommandHandler<MakeOfferCommand, MakeOfferCommandResponse>
{
    private readonly IOfferService _offerService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;
    
    public MakeOfferCommandHandler(ILocalizationService localizationService, IOfferService offerService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _offerService = offerService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<MakeOfferCommandResponse> Handle(MakeOfferCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _offerService.MakeOfferAsync(companyId, request.MakeOffer);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("MadeOffer"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
