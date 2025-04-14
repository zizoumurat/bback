using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.ContractFeatures.Commands.ApproveReject;
public class ApproveRejectContractCommandHandler : ICommandHandler<ApproveRejectContractCommand, ApproveRejectContractCommandResponse>
{
    private readonly IContractService _contractService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public ApproveRejectContractCommandHandler(ILocalizationService localizationService, IContractService contractService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _contractService = contractService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<ApproveRejectContractCommandResponse> Handle(ApproveRejectContractCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();

            int userId = _tokenService.GetUserIdByToken();

            await _transactionManager.BeginTransactionAsync();

            await _contractService.ApproveRejectContract(userId, companyId, request.Request);

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
