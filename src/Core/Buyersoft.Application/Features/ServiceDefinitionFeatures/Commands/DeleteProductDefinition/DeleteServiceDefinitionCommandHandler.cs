using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.ServiceDefinitionFeatures.Commands.DeleteServiceDefinition;

public class DeleteServiceDefinitionCommandHandler : ICommandHandler<DeleteServiceDefinitionCommand, DeleteServiceDefinitionCommandResponse>
{
    private readonly IServiceDefinitionService _ServiceDefinitionService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteServiceDefinitionCommandHandler(ILocalizationService localizationService, IServiceDefinitionService ServiceDefinitionService, ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _localizationService = localizationService;
        _ServiceDefinitionService = ServiceDefinitionService;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteServiceDefinitionCommandResponse> Handle(DeleteServiceDefinitionCommand request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        await _ServiceDefinitionService.DeleteAsync(request.Id, companyId);

        await _unitOfWork.SaveChangesAsync();

        return new(_localizationService.GetLocalizedString("ServiceDefinitionDeleted"));
    }
}
