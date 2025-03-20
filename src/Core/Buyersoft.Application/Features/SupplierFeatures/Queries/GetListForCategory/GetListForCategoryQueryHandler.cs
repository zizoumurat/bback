using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.BrancheFeatures.Queries.GetListForCategory;

public sealed class GetListForCategoryQueryHandler : IQueryHandler<GetListForCategoryQuery, GetListForCategoryQueryResponse>
{
    private readonly ISupplierService _supplierService;
    private readonly ITokenService _tokenService;

    public GetListForCategoryQueryHandler(ISupplierService supplierService, ITokenService tokenService)
    {
        _supplierService = supplierService;
        _tokenService = tokenService;
    }

    public async Task<GetListForCategoryQueryResponse> Handle(GetListForCategoryQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _supplierService.GetListForCategory(companyId, request.requestGroupId, request.cityId, request.channelType);

        return new(result);
    }
}