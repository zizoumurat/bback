using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.OrderFeatures.Queries.GetAllOrder;

public sealed class GetAllOrderQueryHandler : IQueryHandler<GetAllOrderQuery, GetAllOrderQueryResponse>
{
    private readonly IOrderService _OrderService;
    private readonly ITokenService _tokenService;

    public GetAllOrderQueryHandler(IOrderService OrderService, ITokenService tokenService)
    {
        _OrderService = OrderService;
        _tokenService = tokenService;
    }

    public async Task<GetAllOrderQueryResponse> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();
        int supplierId = _tokenService.GetSupplierIdByToken();

        var result = await _OrderService.GetAllAsync(companyId, supplierId, request.filter, request.pagination);

        return new(result);
    }
}