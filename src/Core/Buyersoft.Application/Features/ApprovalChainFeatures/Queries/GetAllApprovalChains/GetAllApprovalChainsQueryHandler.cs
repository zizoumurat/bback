using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.ApprovalChainFeatures.Queries.GetAllApprovalChains;

public sealed class GetAllApprovalChainsQueryHandler : IQueryHandler<GetAllApprovalChainsQuery, GetAllApprovalChainsQueryResponse>
{
    private readonly IApprovalChainService _approvalChainService;
    private readonly ITokenService _tokenService;

    public GetAllApprovalChainsQueryHandler(IApprovalChainService approvalChainService, ITokenService tokenService)
    {
        _approvalChainService = approvalChainService;
        _tokenService = tokenService;
    }

    public async Task<GetAllApprovalChainsQueryResponse> Handle(GetAllApprovalChainsQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _approvalChainService.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}