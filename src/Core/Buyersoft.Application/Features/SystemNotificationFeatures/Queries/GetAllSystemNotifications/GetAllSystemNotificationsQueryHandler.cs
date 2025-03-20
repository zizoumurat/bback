using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.SystemNotificationFeatures.Queries.GetAllSystemNotifications;

public sealed class GetAllSystemNotificationsQueryHandler : IQueryHandler<GetAllSystemNotificationsQuery, GetAllSystemNotificationsQueryResponse>
{
    private readonly ISystemNotificationService _SystemNotificationService;
    private readonly ITokenService _tokenService;

    public GetAllSystemNotificationsQueryHandler(ISystemNotificationService SystemNotificationService, ITokenService tokenService)
    {
        _SystemNotificationService = SystemNotificationService;
        _tokenService = tokenService;
    }

    public async Task<GetAllSystemNotificationsQueryResponse> Handle(GetAllSystemNotificationsQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _SystemNotificationService.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}