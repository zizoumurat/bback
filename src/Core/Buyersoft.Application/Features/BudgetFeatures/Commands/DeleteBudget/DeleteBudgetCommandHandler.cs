using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.BudgetFeatures.Commands.DeleteBudget;

public class DeleteBudgetCommandHandler : ICommandHandler<DeleteBudgetCommand, DeleteBudgetCommandResponse>
{
    private readonly IBudgetService _budgetService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public DeleteBudgetCommandHandler(ILocalizationService localizationService, IBudgetService budgetService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _budgetService = budgetService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<DeleteBudgetCommandResponse> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _budgetService.DeleteAsync(request.Id, companyId);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("BudgetDeleted"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
