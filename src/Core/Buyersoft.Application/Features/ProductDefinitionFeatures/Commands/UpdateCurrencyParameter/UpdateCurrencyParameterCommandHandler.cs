using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.ProductDefinitionFeatures.Commands.UpdateProductDefinition;
public class UpdateProductDefinitionCommandHandler : ICommandHandler<UpdateProductDefinitionCommand, UpdateProductDefinitionCommandResponse>
{
    private readonly IProductDefinitionService _ProductDefinitionService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductDefinitionCommandHandler(ILocalizationService localizationService, IProductDefinitionService ProductDefinitionService, ITokenService tokenService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _localizationService = localizationService;
        _ProductDefinitionService = ProductDefinitionService;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UpdateProductDefinitionCommandResponse> Handle(UpdateProductDefinitionCommand request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        await _ProductDefinitionService.UpdateAsync(companyId, request.ProductDefinition);

        await _unitOfWork.SaveChangesAsync();

        return new(_localizationService.GetLocalizedString("ProductDefinitionUpdated"));
    }
}
