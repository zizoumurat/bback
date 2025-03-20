using Buyersoft.Application.Features.RequestGroupFeatures.Commands.CreateRequestGroup;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.RequestGroupFeatures.Commands.CreateRequest;
public class CreateRequestGroupCommandHandler : ICommandHandler<CreateRequestGroupCommand, CreateRequestGroupCommandResponse>
{
    private readonly IRequestGroupService _requestGroupService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateRequestGroupCommandHandler(ILocalizationService localizationService, IRequestGroupService requestGroupService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _requestGroupService = requestGroupService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateRequestGroupCommandResponse> Handle(CreateRequestGroupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _requestGroupService.AddAsync(companyId, request.RequestGroup);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("RequestGroupCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
