using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.ContractFeatures.Commands.ApproveReject;
public class AddCommentCommandHandler : ICommandHandler<AddCommentCommand, AddCommentCommandResponse>
{
    private readonly IContractService _contractService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public AddCommentCommandHandler(ILocalizationService localizationService, IContractService contractService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _contractService = contractService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<AddCommentCommandResponse> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();

            int userId = _tokenService.GetUserIdByToken();

            var userName = _tokenService.GetUserNameByToken();

            await _transactionManager.BeginTransactionAsync();

            await _contractService.AddComment(userId, userName, request.Request);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("CommentAdded"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
