using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.LocationFeatures.Commands.UpdateLocation;
public class UpdateLocationCommandHandler : ICommandHandler<UpdateLocationCommand, UpdateLocationCommandResponse>
{
    private readonly ILocationService _locationService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public UpdateLocationCommandHandler(ILocalizationService localizationService, ILocationService locationService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _locationService = locationService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<UpdateLocationCommandResponse> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _locationService.UpdateAsync(companyId, request.Location);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("LocationUpdated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
