using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.ServiceDefinitionFeatures.Queries.GetAllServiceDefinitions;

public sealed class GetAllServiceDefinitionsQueryHandler : IQueryHandler<GetAllServiceDefinitionsQuery, GetAllServiceDefinitionsQueryResponse>
{
    private readonly IServiceDefinitionService _ServiceDefinitionService;
    private readonly ITokenService _tokenService;

    public GetAllServiceDefinitionsQueryHandler(IServiceDefinitionService ServiceDefinitionService, ITokenService tokenService)
    {
        _ServiceDefinitionService = ServiceDefinitionService;
        _tokenService = tokenService;
    }

    public async Task<GetAllServiceDefinitionsQueryResponse> Handle(GetAllServiceDefinitionsQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _ServiceDefinitionService.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}