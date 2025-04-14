using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.SubCategoryFeatures.Queries.GetAllSubCategories;

public sealed class GetAllSubCategoriesQueryHandler : IQueryHandler<GetAllSubCategoriesQuery, GetAllSubCategoriesQueryResponse>
{
    private readonly ISubCategoryService _subCategoryService;
    private readonly ITokenService _tokenService;

    public GetAllSubCategoriesQueryHandler(ISubCategoryService subCategoryService, ITokenService tokenService)
    {
        _subCategoryService = subCategoryService;
        _tokenService = tokenService;
    }

    public async Task<GetAllSubCategoriesQueryResponse> Handle(GetAllSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _subCategoryService.GetAllAsync(companyId, request.MainCategoryId);

        return new(result);
    }
}