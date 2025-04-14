using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.BudgetFeatures.Commands.UpdateBudget;

public sealed record UpdateBudgetCommand(BudgetDto Budget) : ICommand<UpdateBudgetCommandResponse>;