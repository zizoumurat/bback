using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.RequestGroupFeatures.Queries.GetAllRequestGroups;

public sealed class GetAllRequestGroupsQueryHandler : IQueryHandler<GetAllRequestGroupsQuery, GetAllRequestGroupsQueryResponse>
{
    private readonly IRequestGroupService _RequestGroupService;
    private readonly ITokenService _tokenService;

    public GetAllRequestGroupsQueryHandler(IRequestGroupService RequestGroupService, ITokenService tokenService)
    {
        _RequestGroupService = RequestGroupService;
        _tokenService = tokenService;
    }

    public async Task<GetAllRequestGroupsQueryResponse> Handle(GetAllRequestGroupsQuery RequestGroup, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _RequestGroupService.GetAllAsync(companyId);

        return new(result);
    }
}