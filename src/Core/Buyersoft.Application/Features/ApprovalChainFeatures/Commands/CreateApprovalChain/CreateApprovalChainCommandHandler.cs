using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.ApprovalChainFeatures.Commands.CreateApprovalChain;
public class CreateApprovalChainCommandHandler : ICommandHandler<CreateApprovalChainCommand, CreateApprovalChainCommandResponse>
{
    private readonly IApprovalChainService _approvalChainService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateApprovalChainCommandHandler(ILocalizationService localizationService, IApprovalChainService ApprovalChainService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _approvalChainService = ApprovalChainService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateApprovalChainCommandResponse> Handle(CreateApprovalChainCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _approvalChainService.AddAsync(companyId, request.ApprovalChain);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("ApprovalChainCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
