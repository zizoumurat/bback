using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.ApproveReject;
public class ApproveRejectCommandHandler : ICommandHandler<ApproveRejectCommand, ApproveRejectCommandResponse>
{
    private readonly IRequestService _requestService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public ApproveRejectCommandHandler(ILocalizationService localizationService, IRequestService requestService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _requestService = requestService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<ApproveRejectCommandResponse> Handle(ApproveRejectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();

            int userId = _tokenService.GetUserIdByToken();

            await _transactionManager.BeginTransactionAsync();

            await _requestService.ApproveRejectRequest(userId, request.Request);

            await _transactionManager.CommitAsync();

            var resultMessage = request.Request.Status == Domain.Enums.ApprovalStatus.Approved ? "RequestApproved" : "RequestRejected";

            return new(_localizationService.GetLocalizedString(resultMessage));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
