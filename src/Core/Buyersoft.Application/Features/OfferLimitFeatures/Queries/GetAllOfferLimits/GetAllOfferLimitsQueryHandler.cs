using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Queries.GetAllOfferLimits;

public sealed class GetAllOfferLimitsQueryHandler : IQueryHandler<GetAllOfferLimitsQuery, GetAllOfferLimitsQueryResponse>
{
    private readonly IOfferLimitService _OfferLimitService;
    private readonly ITokenService _tokenService;

    public GetAllOfferLimitsQueryHandler(IOfferLimitService OfferLimitService, ITokenService tokenService)
    {
        _OfferLimitService = OfferLimitService;
        _tokenService = tokenService;
    }

    public async Task<GetAllOfferLimitsQueryResponse> Handle(GetAllOfferLimitsQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _OfferLimitService.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}