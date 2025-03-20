using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.BudgetFeatures.Queries.GetAvailableBudgetList;
public sealed record GetAvailableBudgetListQuery() : IQuery<GetAvailableBudgetListQueryResponse>;
