using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.BudgetFeatures.Commands.CreateBudget;
public class CreateBudgetCommandHandler : ICommandHandler<CreateBudgetCommand, CreateBudgetCommandResponse>
{
    private readonly IBudgetService _budgetService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateBudgetCommandHandler(ILocalizationService localizationService, IBudgetService budgetService, ITokenService tokenService, IUnitOfWork unitOfWork, IMapper mapper, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _budgetService = budgetService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateBudgetCommandResponse> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _budgetService.AddAsync(companyId, request.Budget);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("BudgetCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
