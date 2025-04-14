using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.CurrencyParameterFeatures.Queries.GetAllCurrencyParameters;

public sealed class GetAllCurrencyParametersQueryHandler : IQueryHandler<GetAllCurrencyParametersQuery, GetAllCurrencyParametersQueryResponse>
{
    private readonly ICurrencyParameterService _currencyParameterService;
    private readonly ITokenService _tokenService;

    public GetAllCurrencyParametersQueryHandler(ICurrencyParameterService currencyParameterService, ITokenService tokenService)
    {
        _currencyParameterService = currencyParameterService;
        _tokenService = tokenService;
    }

    public async Task<GetAllCurrencyParametersQueryResponse> Handle(GetAllCurrencyParametersQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _currencyParameterService.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}