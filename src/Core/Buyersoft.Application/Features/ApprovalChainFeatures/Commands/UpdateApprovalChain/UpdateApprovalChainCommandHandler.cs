using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.ApprovalChainFeatures.Commands.UpdateApprovalChain;
public class UpdateApprovalChainCommandHandler : ICommandHandler<UpdateApprovalChainCommand, UpdateApprovalChainCommandResponse>
{
    private readonly IApprovalChainService _approvalChainService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService; 
    private readonly ITransactionManager _transactionManager;

    public UpdateApprovalChainCommandHandler(ILocalizationService localizationService, IApprovalChainService ApprovalChainService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _approvalChainService = ApprovalChainService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<UpdateApprovalChainCommandResponse> Handle(UpdateApprovalChainCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _approvalChainService.UpdateAsync(companyId, request.ApprovalChain);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("ApprovalChainUpdated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
