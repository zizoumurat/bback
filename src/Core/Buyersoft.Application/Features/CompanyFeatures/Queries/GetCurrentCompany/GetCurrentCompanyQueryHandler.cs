using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.CompanyFeatures.Queries.GetCurrentCompany;

public sealed class GetCurrentCompanyQueryHandler : IQueryHandler<GetCurrentCompanyQuery, GetCurrentCompanyQueryResponse>
{
    private readonly ICompanyService _companyService;
    private readonly ITokenService _tokenService;

    public GetCurrentCompanyQueryHandler(ICompanyService companyService, ITokenService tokenService)
    {
        _companyService = companyService;
        _tokenService = tokenService;
    }

    public async Task<GetCurrentCompanyQueryResponse> Handle(GetCurrentCompanyQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _companyService.GetCurrentCompany(companyId);

        return new(result);
    }
}