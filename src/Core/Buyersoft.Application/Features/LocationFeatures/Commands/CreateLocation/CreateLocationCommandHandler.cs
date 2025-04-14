using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.LocationFeatures.Commands.CreateLocation;
public class CreateLocationCommandHandler : ICommandHandler<CreateLocationCommand, CreateLocationCommandResponse>
{
    private readonly ILocationService _locationService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateLocationCommandHandler(ILocalizationService localizationService, ILocationService locationService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _locationService = locationService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateLocationCommandResponse> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _locationService.AddAsync(companyId, request.Location);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("LocationCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
