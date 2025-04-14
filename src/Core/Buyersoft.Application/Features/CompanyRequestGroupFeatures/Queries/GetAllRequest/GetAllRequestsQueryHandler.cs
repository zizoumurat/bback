using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.CompanyRequestGroupFeatures.Queries.GetAllCompanyRequestGroups;

public sealed class GetAllCompanyRequestGroupsQueryHandler : IQueryHandler<GetAllCompanyRequestGroupsQuery, GetAllCompanyRequestGroupsQueryResponse>
{
    private readonly ICompanyRequestGroupService _CompanyRequestGroupService;
    private readonly ITokenService _tokenService;

    public GetAllCompanyRequestGroupsQueryHandler(ICompanyRequestGroupService CompanyRequestGroupService, ITokenService tokenService)
    {
        _CompanyRequestGroupService = CompanyRequestGroupService;
        _tokenService = tokenService;
    }

    public async Task<GetAllCompanyRequestGroupsQueryResponse> Handle(GetAllCompanyRequestGroupsQuery Model, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _CompanyRequestGroupService.GetAllAsync(companyId, Model.SubCategoryId);

        return new(result);
    }
}