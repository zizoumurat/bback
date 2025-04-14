using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.SupplierActionFeatures.Queries.GetAllByCompany;

public sealed class GetAllByCompanyQueryHandler : IQueryHandler<GetAllByCompanyQuery, GetAllByCompanyQueryResponse>
{
    private readonly ISupplierActionService _supplierActionService;
    private readonly ITokenService _tokenService;

    public GetAllByCompanyQueryHandler(ISupplierActionService supplierActionService, ITokenService tokenService)
    {
        _supplierActionService = supplierActionService;
        _tokenService = tokenService;
    }

    public async Task<GetAllByCompanyQueryResponse> Handle(GetAllByCompanyQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _supplierActionService.GetListAsync(companyId, request.SupplierId);

        return new(result);
    }
}