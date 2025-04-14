using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.CancelBidCollection;
public class CancelBidCollectionCommandHandler : ICommandHandler<CancelBidCollectionCommand, CancelBidCollectionCommandResponse>
{
    private readonly IRequestService _requestService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;
    public CancelBidCollectionCommandHandler(ILocalizationService localizationService, IRequestService requestService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _requestService = requestService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CancelBidCollectionCommandResponse> Handle(CancelBidCollectionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();

            int userId = _tokenService.GetUserIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _requestService.CancelBidCollection(request.Model);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("RequestCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
