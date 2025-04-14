using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.BrancheFeatures.Queries.GetAllBranches;

public sealed class GetAllBranchesQueryHandler : IQueryHandler<GetAllBranchesQuery, GetAllBranchesQueryResponse>
{
    private readonly IBranchService _branchService;
    private readonly ITokenService _tokenService;

    public GetAllBranchesQueryHandler(IBranchService branchService, ITokenService tokenService)
    {
        _branchService = branchService;
        _tokenService = tokenService;
    }

    public async Task<GetAllBranchesQueryResponse> Handle(GetAllBranchesQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _branchService.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}