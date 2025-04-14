using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.CategoryFeatures.Queries.GetAllCategories;

public sealed class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, GetAllCategoriesQueryResponse>
{
    private readonly ICategoryService _categoryService;
    private readonly ITokenService _tokenService;

    public GetAllCategoriesQueryHandler(ICategoryService Categorieservice, ITokenService tokenService)
        {
        _categoryService = Categorieservice;
        _tokenService = tokenService;
    }

    public async Task<GetAllCategoriesQueryResponse> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _categoryService.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}