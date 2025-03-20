using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.ReverseAuctionFeatures.Queries.GetAllReverseAuctions;

public sealed class GetAllReverseAuctionsQueryHandler : IQueryHandler<GetAllReverseAuctionsQuery, GetAllReverseAuctionsQueryResponse>
{
    private readonly IReverseAuctionService _reverseAuctionService;
    private readonly ITokenService _tokenService;

    public GetAllReverseAuctionsQueryHandler(IReverseAuctionService reverseAuctionService, ITokenService tokenService)
    {
        _reverseAuctionService = reverseAuctionService;
        _tokenService = tokenService;
    }

    public async Task<GetAllReverseAuctionsQueryResponse> Handle(GetAllReverseAuctionsQuery ReverseAuction, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _reverseAuctionService.GetAllAsync(companyId, ReverseAuction.filter, ReverseAuction.pagination);

        return new(result);
    }
}