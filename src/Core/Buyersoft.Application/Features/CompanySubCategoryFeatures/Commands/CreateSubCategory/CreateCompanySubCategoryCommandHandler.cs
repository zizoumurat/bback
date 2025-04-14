using AutoMapper;
using Buyersoft.Application.Features.CompanyCompanySubCategoryFeatures.Commands.CreateCompanySubCategory;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.CompanySubCategoryFeatures.Commands.CreateCompanySubCategory;
public class CreateCompanySubCategoryCommandHandler : ICommandHandler<CreateCompanySubCategoryCommand, CreateCompanySubCategoryCommandResponse>
{
    private readonly ICompanySubCategoryService _CompanySubCategoryService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateCompanySubCategoryCommandHandler(ILocalizationService localizationService, ICompanySubCategoryService CompanySubCategoryService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _CompanySubCategoryService = CompanySubCategoryService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateCompanySubCategoryCommandResponse> Handle(CreateCompanySubCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _CompanySubCategoryService.AddAsync(companyId, request.CompanySubCategory);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("CompanySubCategoryCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
