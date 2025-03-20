using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.BudgetFeatures.Commands.CreateBudget;

public sealed record CreateBudgetCommand(BudgetDto Budget) : ICommand<CreateBudgetCommandResponse>;