using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.BudgetFeatures.Commands.UpdateBudget;
public class UpdateBudgetCommandHandler : ICommandHandler<UpdateBudgetCommand, UpdateBudgetCommandResponse>
{
    private readonly IBudgetService _budgetService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public UpdateBudgetCommandHandler(ILocalizationService localizationService, IBudgetService budgetService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _budgetService = budgetService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<UpdateBudgetCommandResponse> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _budgetService.UpdateAsync(companyId, request.Budget);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("BudgetUpdated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
