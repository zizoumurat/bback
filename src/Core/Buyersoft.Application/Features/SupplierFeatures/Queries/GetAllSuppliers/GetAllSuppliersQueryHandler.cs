using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.BrancheFeatures.Queries.GetAllSuppliers;

public sealed class GetAllSuppliersQueryHandler : IQueryHandler<GetAllSuppliersQuery, GetAllSuppliersQueryResponse>
{
    private readonly ISupplierService _supplierService;
    private readonly ITokenService _tokenService;

    public GetAllSuppliersQueryHandler(ISupplierService supplierService, ITokenService tokenService)
    {
        _supplierService = supplierService;
        _tokenService = tokenService;
    }

    public async Task<GetAllSuppliersQueryResponse> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _supplierService.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}