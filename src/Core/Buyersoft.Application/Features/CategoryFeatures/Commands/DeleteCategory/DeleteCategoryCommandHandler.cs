using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.CategoryFeatures.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand, DeleteCategoryCommandResponse>
{
    private readonly ICategoryService _categoryService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public DeleteCategoryCommandHandler(ILocalizationService localizationService, ICategoryService categoryService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _categoryService = categoryService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _categoryService.DeleteAsync(request.Id, companyId);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("CategoryDeleted"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
