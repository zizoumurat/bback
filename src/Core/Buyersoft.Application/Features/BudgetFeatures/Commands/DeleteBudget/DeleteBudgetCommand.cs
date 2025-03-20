using Buyersoft.Application.Features.BudgetFeatures.Commands.DeleteBudget;
using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.BudgetFeatures.Commands.DeleteBudget;
public sealed record DeleteBudgetCommand(int Id) : ICommand<DeleteBudgetCommandResponse>;
