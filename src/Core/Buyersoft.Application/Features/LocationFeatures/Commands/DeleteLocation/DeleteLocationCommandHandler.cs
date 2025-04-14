using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.LocationFeatures.Commands.DeleteLocation;

public class DeleteLocationCommandHandler : ICommandHandler<DeleteLocationCommand, DeleteLocationCommandResponse>
{
    private readonly ILocationService _locationService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public DeleteLocationCommandHandler(ILocalizationService localizationService, ILocationService locationService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _locationService = locationService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<DeleteLocationCommandResponse> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _locationService.DeleteAsync(request.Id, companyId);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("LocationDeleted"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
