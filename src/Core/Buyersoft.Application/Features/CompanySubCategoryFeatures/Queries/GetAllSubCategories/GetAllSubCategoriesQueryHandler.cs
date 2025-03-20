using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.CompanySubCategoryFeatures.Queries.GetAllCompanySubCategories;

public sealed class GetAllCompanySubCategoriesQueryHandler : IQueryHandler<GetAllCompanySubCategoriesQuery, GetAllCompanySubCategoriesQueryResponse>
{
    private readonly ICompanySubCategoryService _CompanySubCategoryService;
    private readonly ITokenService _tokenService;

    public GetAllCompanySubCategoriesQueryHandler(ICompanySubCategoryService CompanySubCategoryService, ITokenService tokenService)
    {
        _CompanySubCategoryService = CompanySubCategoryService;
        _tokenService = tokenService;
    }

    public async Task<GetAllCompanySubCategoriesQueryResponse> Handle(GetAllCompanySubCategoriesQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _CompanySubCategoryService.GetAllAsync(companyId, request.MainCategoryId);

        return new(result);
    }
}