using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.MainCategoryFeatures.Commands.UpdateMainCategory;
public class UpdateMainCategoryCommandHandler : ICommandHandler<UpdateMainCategoryCommand, UpdateMainCategoryCommandResponse>
{
    private readonly IMainCategorieservice _mainCategorieservice;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;
    
    public UpdateMainCategoryCommandHandler(ILocalizationService localizationService, IMainCategorieservice mainCategorieservice, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _mainCategorieservice = mainCategorieservice;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<UpdateMainCategoryCommandResponse> Handle(UpdateMainCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _mainCategorieservice.UpdateAsync(companyId, request.MainCategory);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("MainCategoryUpdated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
