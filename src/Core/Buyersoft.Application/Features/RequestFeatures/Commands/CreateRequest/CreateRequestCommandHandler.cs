using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.CreateRequest;
public class CreateRequestCommandHandler : ICommandHandler<CreateRequestCommand, CreateRequestCommandResponse>
{
    private readonly IRequestService _requestService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateRequestCommandHandler(ILocalizationService localizationService, IRequestService requestService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _requestService = requestService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateRequestCommandResponse> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();

            int userId = _tokenService.GetUserIdByToken();

            string userName = _tokenService.GetUserNameByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _requestService.AddAsync(companyId, userId, request.Request, userName);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("RequestCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
