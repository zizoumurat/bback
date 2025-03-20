using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.BudgetFeatures.Queries.GetAvailableBudgetList;

public sealed class GetAvailableBudgetListQueryHandler : IQueryHandler<GetAvailableBudgetListQuery, GetAvailableBudgetListQueryResponse>
{
    private readonly IBudgetService _BudgetService;
    private readonly ITokenService _tokenService;

    public GetAvailableBudgetListQueryHandler(IBudgetService BudgetService, ITokenService tokenService)
    {
        _BudgetService = BudgetService;
        _tokenService = tokenService;
    }

    public async Task<GetAvailableBudgetListQueryResponse> Handle(GetAvailableBudgetListQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _BudgetService.GetAvailableList(companyId);

        return new(result);
    }
}