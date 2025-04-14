using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.RequestFeatures.Queries.GetAllRequests;

public sealed class GetAllRequestsQueryHandler : IQueryHandler<GetAllRequestsQuery, GetAllRequestsQueryResponse>
{
    private readonly IRequestService _RequestService;
    private readonly ITokenService _tokenService;

    public GetAllRequestsQueryHandler(IRequestService RequestService, ITokenService tokenService)
    {
        _RequestService = RequestService;
        _tokenService = tokenService;
    }

    public async Task<GetAllRequestsQueryResponse> Handle(GetAllRequestsQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();
        int userId = _tokenService.GetUserIdByToken();

        var result = await _RequestService.GetAllAsync(companyId, userId, request.filter, request.pagination);

        return new(result);
    }
}