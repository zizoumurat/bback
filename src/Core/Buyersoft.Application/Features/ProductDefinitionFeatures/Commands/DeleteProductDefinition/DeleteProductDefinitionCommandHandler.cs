using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.ProductDefinitionFeatures.Commands.DeleteProductDefinition;

public class DeleteProductDefinitionCommandHandler : ICommandHandler<DeleteProductDefinitionCommand, DeleteProductDefinitionCommandResponse>
{
    private readonly IProductDefinitionService _ProductDefinitionService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductDefinitionCommandHandler(ILocalizationService localizationService, IProductDefinitionService ProductDefinitionService, ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _localizationService = localizationService;
        _ProductDefinitionService = ProductDefinitionService;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteProductDefinitionCommandResponse> Handle(DeleteProductDefinitionCommand request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        await _ProductDefinitionService.DeleteAsync(request.Id, companyId);

        await _unitOfWork.SaveChangesAsync();

        return new(_localizationService.GetLocalizedString("ProductDefinitionDeleted"));
    }
}
