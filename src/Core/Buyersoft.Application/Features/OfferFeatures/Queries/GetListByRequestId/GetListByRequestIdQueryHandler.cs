using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.RequestFeatures.Queries.GetListByRequestId;

public sealed class GetListByRequestIdQueryHandler : IQueryHandler<GetListByRequestIdQuery, GetListByRequestIdQueryResponse>
{
    private readonly IOfferService _offerService;
    private readonly ITokenService _tokenService;

    public GetListByRequestIdQueryHandler(IOfferService offerService, ITokenService tokenService)
    {
        _offerService = offerService;
        _tokenService = tokenService;
    }

    public async Task<GetListByRequestIdQueryResponse> Handle(GetListByRequestIdQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _offerService.GetOfferListByRequest(companyId, request.Id);

        return new(result);
    }
}