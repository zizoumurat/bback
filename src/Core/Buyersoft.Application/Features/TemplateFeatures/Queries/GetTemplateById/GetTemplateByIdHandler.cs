using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.TemplateFeatures.Queries.GetTemplateById;

public sealed class GetTemplateByIdHandler : IQueryHandler<GetTemplateByIdQuery, GetTemplateByIdResponse>
{
    private readonly ITemplateService _templateService;
    private readonly ITokenService _tokenService;

    public GetTemplateByIdHandler(ITemplateService templateService, ITokenService tokenService)
    {
        _templateService = templateService;
        _tokenService = tokenService;
    }

    public async Task<GetTemplateByIdResponse> Handle(GetTemplateByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _templateService.GetById(request.id);

        return new(result);
    }
}