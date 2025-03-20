using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.CategoryFeatures.Commands.CreateCategory;
public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
{
    private readonly ICategoryService _categoryService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;
    
    public CreateCategoryCommandHandler(ILocalizationService localizationService, ICategoryService Categorieservice, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _categoryService = Categorieservice;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _categoryService.AddAsync(companyId, request.Category);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("CategoryCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
