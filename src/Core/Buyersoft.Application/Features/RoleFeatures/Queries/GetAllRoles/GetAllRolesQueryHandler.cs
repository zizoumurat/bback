using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.RoleFeatures.Queries.GetAllRoles;

public sealed class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, GetAllRolesQueryResponse>
{
    private readonly IRoleService _roleService;
    private readonly ITokenService _tokenService;

    public GetAllRolesQueryHandler(IRoleService roleservice, ITokenService tokenService)
    {
        _roleService = roleservice;
        _tokenService = tokenService;
    }

    public async Task<GetAllRolesQueryResponse> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _roleService.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}