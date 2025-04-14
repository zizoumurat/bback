using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.CategoryFeatures.Queries.GetCategoryIdByParameters;

public sealed class GetCategoryIdByParametersQueryHandler : IQueryHandler<GetCategoryIdByParametersQuery, GetCategoryIdByParametersQueryResponse>
{
    private readonly ICategoryService _categoryService;
    private readonly ITokenService _tokenService;

    public GetCategoryIdByParametersQueryHandler(ICategoryService Categorieservice, ITokenService tokenService)
        {
        _categoryService = Categorieservice;
        _tokenService = tokenService;
    }

    public async Task<GetCategoryIdByParametersQueryResponse> Handle(GetCategoryIdByParametersQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _categoryService.GetCategoryIdByParameters(companyId, request.filter);

        return new(result);
    }
}