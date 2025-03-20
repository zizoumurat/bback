using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.UserFeatures.Queries.GetPaginationList;

public sealed class GetPaginationListQueryHandler : IQueryHandler<GetPaginationListQuery, GetPaginationListQueryResponse>
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public GetPaginationListQueryHandler(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    public async Task<GetPaginationListQueryResponse> Handle(GetPaginationListQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _userService.GetPaginationListAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}