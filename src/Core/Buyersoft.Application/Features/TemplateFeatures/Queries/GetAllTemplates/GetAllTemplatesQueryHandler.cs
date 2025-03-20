using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.TemplateFeatures.Queries.GetAllTemplates;

public sealed class GetAllTemplatesQueryHandler : IQueryHandler<GetAllTemplatesQuery, GetAllTemplatesQueryResponse>
{
    private readonly ITemplateService _TemplateService;
    private readonly ITokenService _tokenService;

    public GetAllTemplatesQueryHandler(ITemplateService TemplateService, ITokenService tokenService)
    {
        _TemplateService = TemplateService;
        _tokenService = tokenService;
    }

    public async Task<GetAllTemplatesQueryResponse> Handle(GetAllTemplatesQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _TemplateService.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}