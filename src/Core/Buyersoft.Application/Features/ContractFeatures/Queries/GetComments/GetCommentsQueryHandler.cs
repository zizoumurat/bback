using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.BrancheFeatures.Queries.GetComments;

public sealed class GetCommentsQueryHandler : IQueryHandler<GetCommentsQuery, GetCommentsQueryResponse>
{
    private readonly IContractService _contractService;
    private readonly ITokenService _tokenService;

    public GetCommentsQueryHandler(IContractService contractService, ITokenService tokenService)
    {
        _contractService = contractService;
        _tokenService = tokenService;
    }

    public async Task<GetCommentsQueryResponse> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();
        int userId = _tokenService.GetUserIdByToken();

        var result = await _contractService.GetCommentList(request.contractId);

        return new(result);
    }
}