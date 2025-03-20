using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.MainCategoryFeatures.Queries.GetAllMainCategories;

public sealed class GetAllMainCategoriesQueryHandler : IQueryHandler<GetAllMainCategoriesQuery, GetAllMainCategoriesQueryResponse>
{
    private readonly IMainCategorieservice _MainCategorieservice;
    private readonly ITokenService _tokenService;

    public GetAllMainCategoriesQueryHandler(IMainCategorieservice MainCategorieservice, ITokenService tokenService)
    {
        _MainCategorieservice = MainCategorieservice;
        _tokenService = tokenService;
    }

    public async Task<GetAllMainCategoriesQueryResponse> Handle(GetAllMainCategoriesQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _MainCategorieservice.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}