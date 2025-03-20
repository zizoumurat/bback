using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.ProductDefinitionFeatures.Queries.GetAllProductDefinitions;

public sealed class GetAllProductDefinitionsQueryHandler : IQueryHandler<GetAllProductDefinitionsQuery, GetAllProductDefinitionsQueryResponse>
{
    private readonly IProductDefinitionService _productDefinitionService;
    private readonly ITokenService _tokenService;

    public GetAllProductDefinitionsQueryHandler(IProductDefinitionService productDefinitionService, ITokenService tokenService)
    {
        _productDefinitionService = productDefinitionService;
        _tokenService = tokenService;
    }

    public async Task<GetAllProductDefinitionsQueryResponse> Handle(GetAllProductDefinitionsQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _productDefinitionService.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}