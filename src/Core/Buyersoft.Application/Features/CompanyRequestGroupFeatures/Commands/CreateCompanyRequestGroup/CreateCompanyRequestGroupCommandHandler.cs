using Buyersoft.Application.Features.CompanyRequestGroupFeatures.Commands.CreateCompanyRequestGroup;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.CompanyRequestGroupFeatures.Commands.CreateCompanyRequestGroup;
public class CreateCompanyRequestGroupCommandHandler : ICommandHandler<CreateCompanyRequestGroupCommand, CreateCompanyRequestGroupCommandResponse>
{
    private readonly ICompanyRequestGroupService _CompanyRequestGroupService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateCompanyRequestGroupCommandHandler(ILocalizationService localizationService, ICompanyRequestGroupService CompanyRequestGroupService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _CompanyRequestGroupService = CompanyRequestGroupService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateCompanyRequestGroupCommandResponse> Handle(CreateCompanyRequestGroupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _CompanyRequestGroupService.AddAsync(companyId, request.CompanyRequestGroup);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("CompanyRequestGroupCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
