using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.BudgetFeatures.Queries.GetAllBudgets;
public sealed record GetAllBudgetsQuery(BudgetFilterDto filter, PageRequest pagination) : IQuery<GetAllBudgetsQueryResponse>;
