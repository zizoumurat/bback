using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.ServiceDefinitionFeatures.Commands.UpdateServiceDefinition;
public class UpdateServiceDefinitionCommandHandler : ICommandHandler<UpdateServiceDefinitionCommand, UpdateServiceDefinitionCommandResponse>
{
    private readonly IServiceDefinitionService _ServiceDefinitionService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateServiceDefinitionCommandHandler(ILocalizationService localizationService, IServiceDefinitionService ServiceDefinitionService, ITokenService tokenService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _localizationService = localizationService;
        _ServiceDefinitionService = ServiceDefinitionService;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UpdateServiceDefinitionCommandResponse> Handle(UpdateServiceDefinitionCommand request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        await _ServiceDefinitionService.UpdateAsync(companyId, request.ServiceDefinition);

        await _unitOfWork.SaveChangesAsync();

        return new(_localizationService.GetLocalizedString("ServiceDefinitionUpdated"));
    }
}
