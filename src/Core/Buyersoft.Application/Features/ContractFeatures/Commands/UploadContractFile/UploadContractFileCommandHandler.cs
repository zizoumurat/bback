using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.ContractFeatures.Commands.UploadContractFile;
public class UploadContractFileCommandHandler : ICommandHandler<UploadContractFileCommand, UploadContractFileCommandResponse>
{
    private readonly IContractService _contractService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public UploadContractFileCommandHandler(ILocalizationService localizationService, IContractService contractService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _contractService = contractService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<UploadContractFileCommandResponse> Handle(UploadContractFileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            int userId = _tokenService.GetUserIdByToken();

            await _transactionManager.BeginTransactionAsync();

            await _contractService.UploadContractFileAsync(userId, companyId, request.Model);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("contractDocumentUploaded"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
