using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.BudgetFeatures.Queries.GetAllBudgets;

public sealed record GetAllBudgetsQueryResponse(PaginatedList<BudgetListDto> result);
