using AutoMapper;
using Buyersoft.Application.Features.ServiceDefinitionFeatures.Commands.CreateServiceDefinition;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.ServiceDefinitionFeatures.Commands.ServiceDefinition;
public class ServiceDefinitionCommandHandler : ICommandHandler<CreateServiceDefinitionCommand, CreateServiceDefinitionCommandResponse>
{
    private readonly IServiceDefinitionService _ServiceDefinitionService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ServiceDefinitionCommandHandler(ILocalizationService localizationService, IServiceDefinitionService ServiceDefinitionService, ITokenService tokenService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _localizationService = localizationService;
        _ServiceDefinitionService = ServiceDefinitionService;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateServiceDefinitionCommandResponse> Handle(CreateServiceDefinitionCommand request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        await _ServiceDefinitionService.AddAsync(companyId, request.ServiceDefinition);

        await _unitOfWork.SaveChangesAsync();

        return new(_localizationService.GetLocalizedString("ServiceDefinitionCreated"));
    }
}
