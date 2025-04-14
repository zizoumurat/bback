using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.TemplateFeatures.Commands.DeleteTemplate;

public class DeleteTemplateCommandHandler : ICommandHandler<DeleteTemplateCommand, DeleteTemplateCommandResponse>
{
    private readonly ITemplateService _templateService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public DeleteTemplateCommandHandler(ILocalizationService localizationService, ITemplateService templateService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _templateService = templateService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<DeleteTemplateCommandResponse> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _templateService.DeleteAsync(request.Id, companyId);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("TemplateDeleted"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
