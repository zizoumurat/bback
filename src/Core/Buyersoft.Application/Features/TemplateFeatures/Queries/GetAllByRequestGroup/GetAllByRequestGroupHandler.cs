using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.TemplateFeatures.Queries.GetAllByRequestGroup;

public sealed class GetAllByRequestGroupHandler : IQueryHandler<GetAllByRequestGroupQuery, GetAllByRequestGroupResponse>
{
    private readonly ITemplateService _templateService;
    private readonly ITokenService _tokenService;

    public GetAllByRequestGroupHandler(ITemplateService templateService, ITokenService tokenService)
    {
        _templateService = templateService;
        _tokenService = tokenService;
    }

    public async Task<GetAllByRequestGroupResponse> Handle(GetAllByRequestGroupQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _templateService.GetAllByRequestGroupAsync(request.requestGrouopId);

        return new(result);
    }
}