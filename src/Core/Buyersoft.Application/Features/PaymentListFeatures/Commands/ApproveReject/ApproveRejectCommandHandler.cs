using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.PaymentListFeatures.Commands.CreatePaymentList;
public class ApproveRejectCommandHandler : ICommandHandler<ApproveRejectCommand, ApproveRejectCommandResponse>
{
    private readonly IPaymentListService _paymentListService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public ApproveRejectCommandHandler(ILocalizationService localizationService, IPaymentListService paymentListService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _paymentListService = paymentListService;
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

            await _paymentListService.ApproveRejectPaymentList(userId, companyId, request.Request);

            await _transactionManager.CommitAsync();

            var resultMessage = request.Request.Status == Domain.Enums.ApprovalStatus.Approved ? "ContractApproved" : "ContractRejected";

            return new(_localizationService.GetLocalizedString(resultMessage));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
