using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.ReverseAuctionFeatures.Queries.GetReverseAuctionById;

public sealed class GetReverseAuctionByIdHandler : IQueryHandler<GetReverseAuctionByIdQuery, GetReverseAuctionByIdResponse>
{
    private readonly IReverseAuctionService _reverseAuctionService;
    private readonly ITokenService _tokenService;

    public GetReverseAuctionByIdHandler(IReverseAuctionService reverseAuctionService, ITokenService tokenService)
    {
        _reverseAuctionService = reverseAuctionService;
        _tokenService = tokenService;
    }

    public async Task<GetReverseAuctionByIdResponse> Handle(GetReverseAuctionByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _reverseAuctionService.GetById(request.id);

        return new(result);
    }
}