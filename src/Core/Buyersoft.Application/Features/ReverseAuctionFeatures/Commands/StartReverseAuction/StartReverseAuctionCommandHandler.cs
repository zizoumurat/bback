using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.ReverseAuctionFeatures.Commands.StartReverseAuction;
public class StartReverseAuctionCommandHandler : ICommandHandler<StartReverseAuctionCommand, StartReverseAuctionCommandResponse>
{
    private readonly IRequestService _requestService;
    private readonly IReverseAuctionService _reverseAuctionService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public StartReverseAuctionCommandHandler(ILocalizationService localizationService, IRequestService requestService, ITokenService tokenService, ITransactionManager transactionManager, IReverseAuctionService reverseAuctionService)
    {
        _localizationService = localizationService;
        _requestService = requestService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
        _reverseAuctionService = reverseAuctionService;
    }

    public async Task<StartReverseAuctionCommandResponse> Handle(StartReverseAuctionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();

            int userId = _tokenService.GetUserIdByToken();

            await _transactionManager.BeginTransactionAsync();

            await _reverseAuctionService.ChangeStatu(request.Id, request.Statu, request.remainingSeconds);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("ReverseAuctionStartd"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
