using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.CategoryFeatures.Queries.ExportExcellCategories;

public sealed class ExportExcellCategoriesQueryHandler : IQueryHandler<ExportExcellCategoriesQuery, ExportExcellCategoriesQueryResponse>
{
    private readonly ICategoryService _categoryService;
    private readonly ITokenService _tokenService;

    public ExportExcellCategoriesQueryHandler(ICategoryService Categorieservice, ITokenService tokenService)
        {
        _categoryService = Categorieservice;
        _tokenService = tokenService;
    }

    public async Task<ExportExcellCategoriesQueryResponse> Handle(ExportExcellCategoriesQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _categoryService.GetExcellData(companyId, request.filter);

        return new(result);
    }
}