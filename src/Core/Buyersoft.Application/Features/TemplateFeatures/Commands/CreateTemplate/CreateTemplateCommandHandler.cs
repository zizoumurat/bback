using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.TemplateFeatures.Commands.CreateTemplate;
public class CreateTemplateCommandHandler : ICommandHandler<CreateTemplateCommand, CreateTemplateCommandResponse>
{
    private readonly ITemplateService _templateService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateTemplateCommandHandler(ILocalizationService localizationService, ITemplateService templateService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _templateService = templateService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateTemplateCommandResponse> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _templateService.AddAsync(companyId, request.Template);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("TemplateCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
