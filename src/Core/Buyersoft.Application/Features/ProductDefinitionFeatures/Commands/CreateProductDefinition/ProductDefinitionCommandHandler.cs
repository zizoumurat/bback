using AutoMapper;
using Buyersoft.Application.Features.ProductDefinitionFeatures.Commands.CreateProductDefinition;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.ProductDefinitionFeatures.Commands.ProductDefinition;
public class ProductDefinitionCommandHandler : ICommandHandler<CreateProductDefinitionCommand, CreateProductDefinitionCommandResponse>
{
    private readonly IProductDefinitionService _productDefinitionService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductDefinitionCommandHandler(ILocalizationService localizationService, IProductDefinitionService productDefinitionService, ITokenService tokenService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _localizationService = localizationService;
        _productDefinitionService = productDefinitionService;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateProductDefinitionCommandResponse> Handle(CreateProductDefinitionCommand request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        await _productDefinitionService.AddAsync(companyId, request.ProductDefinition);

        await _unitOfWork.SaveChangesAsync();

        return new(_localizationService.GetLocalizedString("ProductDefinitionCreated"));
    }
}
