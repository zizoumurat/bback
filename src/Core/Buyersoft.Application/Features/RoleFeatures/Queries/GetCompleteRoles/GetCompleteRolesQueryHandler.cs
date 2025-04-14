using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.RoleFeatures.Queries.GetCompleteRoles;

public sealed class GetCompleteRolesQueryHandler : IQueryHandler<GetCompleteRolesQuery, GetCompleteRolesQueryResponse>
{
    private readonly IRoleService _roleService;
    private readonly ITokenService _tokenService;

    public GetCompleteRolesQueryHandler(IRoleService roleservice, ITokenService tokenService)
    {
        _roleService = roleservice;
        _tokenService = tokenService;
    }

    public async Task<GetCompleteRolesQueryResponse> Handle(GetCompleteRolesQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _roleService.GetAllAsync(companyId);

        return new(result);
    }
}