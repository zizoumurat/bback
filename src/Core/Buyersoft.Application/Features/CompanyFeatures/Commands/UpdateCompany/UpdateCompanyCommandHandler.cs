using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.CompanyFeatures.Commands.UpdateCompany;
public class UpdateCompanyCommandHandler : ICommandHandler<UpdateCompanyCommand, UpdateCompanyCommandResponse>
{
    private readonly ICompanyService _companyService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public UpdateCompanyCommandHandler(ILocalizationService localizationService, ICompanyService companyService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _companyService = companyService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<UpdateCompanyCommandResponse> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();

            await _transactionManager.BeginTransactionAsync();

            await _companyService.UpdateAsync(companyId, request.Company);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("CompanyUpdated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
