using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.TemplateFeatures.Commands.UpdateTemplate;
public class UpdateTemplateCommandHandler : ICommandHandler<UpdateTemplateCommand, UpdateTemplateCommandResponse>
{
    private readonly ITemplateService _templateService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public UpdateTemplateCommandHandler(ILocalizationService localizationService, ITemplateService templateService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _templateService = templateService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<UpdateTemplateCommandResponse> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _templateService.UpdateAsync(companyId, request.Template);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("TemplateUpdated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
