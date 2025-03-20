using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.SupplierFeatures.Queries.GetCompanyList;

public sealed class GetCompanyListQueryHandler : IQueryHandler<GetCompanyListQuery, GetCompanyListQueryResponse>
{
    private readonly ISupplierService _supplierService;
    private readonly ITokenService _tokenService;

    public GetCompanyListQueryHandler(ISupplierService supplierService, ITokenService tokenService)
    {
        _supplierService = supplierService;
        _tokenService = tokenService;
    }

    public async Task<GetCompanyListQueryResponse> Handle(GetCompanyListQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _supplierService.GetCompanyPortfolio(companyId, request.filter, request.pagination);

        return new(result);
    }
}