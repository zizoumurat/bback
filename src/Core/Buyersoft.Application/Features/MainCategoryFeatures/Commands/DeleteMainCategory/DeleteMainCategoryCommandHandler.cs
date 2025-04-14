using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.MainCategoryFeatures.Commands.DeleteMainCategory;

public class DeleteMainCategoryCommandHandler : ICommandHandler<DeleteMainCategoryCommand, DeleteMainCategoryCommandResponse>
{
    private readonly IMainCategorieservice _mainCategorieservice;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public DeleteMainCategoryCommandHandler(ILocalizationService localizationService, IMainCategorieservice mainCategorieservice, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _mainCategorieservice = mainCategorieservice;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<DeleteMainCategoryCommandResponse> Handle(DeleteMainCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _mainCategorieservice.DeleteAsync(request.Id, companyId);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("MainCategoryDeleted"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
