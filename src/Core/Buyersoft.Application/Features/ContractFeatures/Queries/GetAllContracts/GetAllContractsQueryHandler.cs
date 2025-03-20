using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.BrancheFeatures.Queries.GetAllContracts;

public sealed class GetAllContractsQueryHandler : IQueryHandler<GetAllContractsQuery, GetAllContractsQueryResponse>
{
    private readonly IContractService _contractService;
    private readonly ITokenService _tokenService;

    public GetAllContractsQueryHandler(IContractService contractService, ITokenService tokenService)
    {
        _contractService = contractService;
        _tokenService = tokenService;
    }

    public async Task<GetAllContractsQueryResponse> Handle(GetAllContractsQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();
        int userId = _tokenService.GetUserIdByToken();

        var result = await _contractService.GetAllAsync(companyId, userId, request.filter, request.pagination);

        return new(result);
    }
}