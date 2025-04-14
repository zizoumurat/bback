using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.AssignManager;
public class AssignManagerCommandHandler : ICommandHandler<AssignManagerCommand, AssignManagerCommandResponse>
{
    private readonly IRequestService _requestService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public AssignManagerCommandHandler(ILocalizationService localizationService, IRequestService requestService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _requestService = requestService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<AssignManagerCommandResponse> Handle(AssignManagerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();

            int userId = _tokenService.GetUserIdByToken();

            await _transactionManager.BeginTransactionAsync();

            await _requestService.AssignManager(companyId, userId, request.Request);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("AssignedAsRequestOwner"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
