using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.OfferFeatures.Commands.RequestRevision;
public class RequestRevisionCommandHandler : ICommandHandler<RequestRevisionCommand, RequestRevisionCommandResponse>
{
    private readonly IOfferService _offerService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;
    
    public RequestRevisionCommandHandler(ILocalizationService localizationService, IOfferService offerService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _offerService = offerService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<RequestRevisionCommandResponse> Handle(RequestRevisionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _offerService.RequestRevision(companyId, request.OfferId);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("RevisionRequestSubmitted"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
