using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.CompanyFeatures.Queries.GetSupplierList;


public sealed class GetSupplierListQueryHander : IQueryHandler<GetSupplierListQuery, GetSupplierListQueryResponse>
{
    private readonly ICompanyService _companyService;
    private readonly ITokenService _tokenService;

    public GetSupplierListQueryHander(ICompanyService companyService, ITokenService tokenService)
    {
        _companyService = companyService;
        _tokenService = tokenService;
    }

    public async Task<GetSupplierListQueryResponse> Handle(GetSupplierListQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _companyService.GetSupplierPortfolio(companyId, request.filter, request.pagination);

        return new(result);
    }
}