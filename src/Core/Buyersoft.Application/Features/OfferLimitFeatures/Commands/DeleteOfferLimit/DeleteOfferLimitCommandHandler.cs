using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.DeleteOfferLimit;

public class DeleteOfferLimitCommandHandler : ICommandHandler<DeleteOfferLimitCommand, DeleteOfferLimitCommandResponse>
{
    private readonly IOfferLimitService _offerLimitService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public DeleteOfferLimitCommandHandler(ILocalizationService localizationService, IOfferLimitService offerLimitService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _offerLimitService = offerLimitService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<DeleteOfferLimitCommandResponse> Handle(DeleteOfferLimitCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _offerLimitService.DeleteAsync(request.Id, companyId);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("OfferLimitDeleted"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
