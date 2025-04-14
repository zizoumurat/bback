using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.BudgetFeatures.Queries.GetAllBudgets;

public sealed class GetAllBudgetsQueryHandler : IQueryHandler<GetAllBudgetsQuery, GetAllBudgetsQueryResponse>
{
    private readonly IBudgetService _BudgetService;
    private readonly ITokenService _tokenService;

    public GetAllBudgetsQueryHandler(IBudgetService BudgetService, ITokenService tokenService)
    {
        _BudgetService = BudgetService;
        _tokenService = tokenService;
    }

    public async Task<GetAllBudgetsQueryResponse> Handle(GetAllBudgetsQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _BudgetService.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}