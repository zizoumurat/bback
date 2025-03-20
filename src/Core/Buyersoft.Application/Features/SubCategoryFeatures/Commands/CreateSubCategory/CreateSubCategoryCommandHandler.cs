using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.SubCategoryFeatures.Commands.CreateSubCategory;
public class CreateSubCategoryCommandHandler : ICommandHandler<CreateSubCategoryCommand, CreateSubCategoryCommandResponse>
{
    private readonly ISubCategoryService _subCategoryService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateSubCategoryCommandHandler(ILocalizationService localizationService, ISubCategoryService subCategoryService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _subCategoryService = subCategoryService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateSubCategoryCommandResponse> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _subCategoryService.AddAsync(companyId, request.SubCategory);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("SubCategoryCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
