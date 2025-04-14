using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.RequestFeatures.Queries.GetAllRequestById;

public sealed class GetAllRequestByIdQueryHandler : IQueryHandler<GetAllRequestByIdQuery, GetAllRequestByIdQueryResponse>
{
    private readonly IRequestService _requestService;
    private readonly ITokenService _tokenService;

    public GetAllRequestByIdQueryHandler(IRequestService requestService, ITokenService tokenService)
    {
        _requestService = requestService;
        _tokenService = tokenService;
    }

    public async Task<GetAllRequestByIdQueryResponse> Handle(GetAllRequestByIdQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _requestService.GetById(companyId, request.Id);

        return new(result);
    }
}