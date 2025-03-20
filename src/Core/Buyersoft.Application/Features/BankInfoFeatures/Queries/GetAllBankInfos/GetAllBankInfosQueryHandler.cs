using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.BankInfoFeatures.Queries.GetAllBankInfos;

public sealed class GetAllBankInfosQueryHandler : IQueryHandler<GetAllBankInfosQuery, GetAllBankInfosQueryResponse>
{
    private readonly IBankInfoService _bankInfoservice;
    private readonly ITokenService _tokenService;

    public GetAllBankInfosQueryHandler(IBankInfoService bankInfoservice, ITokenService tokenService)
    {
        _bankInfoservice = bankInfoservice;
        _tokenService = tokenService;
    }

    public async Task<GetAllBankInfosQueryResponse> Handle(GetAllBankInfosQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _bankInfoservice.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}