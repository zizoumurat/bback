using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.LocationFeatures.Queries.GetAllLocations;

public sealed class GetAllLocationsQueryHandler : IQueryHandler<GetAllLocationsQuery, GetAllLocationsQueryResponse>
{
    private readonly ILocationService _locationService;
    private readonly ITokenService _tokenService;

    public GetAllLocationsQueryHandler(ILocationService locationService, ITokenService tokenService)
    {
        _locationService = locationService;
        _tokenService = tokenService;
    }

    public async Task<GetAllLocationsQueryResponse> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _locationService.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}