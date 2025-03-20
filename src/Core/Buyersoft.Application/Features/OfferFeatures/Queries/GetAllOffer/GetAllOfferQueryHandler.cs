using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.RequestFeatures.Queries.GetAllOffer;

public sealed class GetAllOfferQueryHandler : IQueryHandler<GetAllOfferQuery, GetAllOfferQueryResponse>
{
    private readonly IOfferService _offerService;
    private readonly ITokenService _tokenService;

    public GetAllOfferQueryHandler(IOfferService offerService, ITokenService tokenService)
    {
        _offerService = offerService;
        _tokenService = tokenService;
    }

    public async Task<GetAllOfferQueryResponse> Handle(GetAllOfferQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _offerService.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}