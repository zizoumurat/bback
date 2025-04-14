using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.PaymentListFeatures.Queries.GetAllPaymentLists;

public sealed class GetAllPaymentListsQueryHandler : IQueryHandler<GetAllPaymentListsQuery, GetAllPaymentListsQueryResponse>
{
    private readonly IPaymentListService _paymentListService;
    private readonly ITokenService _tokenService;

    public GetAllPaymentListsQueryHandler(IPaymentListService PaymentListService, ITokenService tokenService)
    {
        _paymentListService = PaymentListService;
        _tokenService = tokenService;
    }

    public async Task<GetAllPaymentListsQueryResponse> Handle(GetAllPaymentListsQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _paymentListService.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}