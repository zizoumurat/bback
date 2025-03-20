using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.BudgetFeatures.Queries.GetAvailableBudgetList;

public sealed record GetAvailableBudgetListQueryResponse(IList<BudgetListDto> result);
