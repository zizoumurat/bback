using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.UpdateRequest;
public class UpdateRequestCommandHandler : ICommandHandler<UpdateRequestCommand, UpdateRequestCommandResponse>
{
    private readonly IRequestService _requestService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public UpdateRequestCommandHandler(ILocalizationService localizationService, IRequestService requestService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _requestService = requestService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<UpdateRequestCommandResponse> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _requestService.UpdateAsync(companyId, request.Request);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("RequestUpdated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
