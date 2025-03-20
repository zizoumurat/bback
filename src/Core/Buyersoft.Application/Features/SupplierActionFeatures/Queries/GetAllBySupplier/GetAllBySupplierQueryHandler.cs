using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.SupplierActionFeatures.Queries.GetAllBySupplier;

public sealed class GetAllBySupplierQueryHandler : IQueryHandler<GetAllBySupplierQuery, GetAllBySupplierQueryResponse>
{
    private readonly ISupplierActionService _supplierActionService;
    private readonly ITokenService _tokenService;

    public GetAllBySupplierQueryHandler(ISupplierActionService supplierActionService, ITokenService tokenService)
    {
        _supplierActionService = supplierActionService;
        _tokenService = tokenService;
    }

    public async Task<GetAllBySupplierQueryResponse> Handle(GetAllBySupplierQuery request, CancellationToken cancellationToken)
    {
        int SupplierId = _tokenService.GetSupplierIdByToken();

        var result = await _supplierActionService.GetListAsync(request.CompanyId, SupplierId);

        return new(result);
    }
}