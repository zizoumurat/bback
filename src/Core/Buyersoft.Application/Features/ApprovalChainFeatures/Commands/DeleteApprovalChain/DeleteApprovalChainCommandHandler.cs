using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.ApprovalChainFeatures.Commands.DeleteApprovalChain;

public class DeleteApprovalChainCommandHandler : ICommandHandler<DeleteApprovalChainCommand, DeleteApprovalChainCommandResponse>
{
    private readonly IApprovalChainService _approvalChainService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public DeleteApprovalChainCommandHandler(ILocalizationService localizationService, IApprovalChainService ApprovalChainService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _approvalChainService = ApprovalChainService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<DeleteApprovalChainCommandResponse> Handle(DeleteApprovalChainCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _approvalChainService.DeleteAsync(request.Id, companyId);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("ApprovalChainDeleted"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
