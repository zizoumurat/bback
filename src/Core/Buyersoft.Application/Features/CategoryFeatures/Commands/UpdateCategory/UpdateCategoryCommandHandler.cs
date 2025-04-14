using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.CategoryFeatures.Commands.UpdateCategory;
public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
{
    private readonly ICategoryService _categorieservice;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;
    public UpdateCategoryCommandHandler(ILocalizationService localizationService, ICategoryService categorieservice, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _categorieservice = categorieservice;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _categorieservice.UpdateAsync(companyId, request.Category);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("CategoryUpdated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
