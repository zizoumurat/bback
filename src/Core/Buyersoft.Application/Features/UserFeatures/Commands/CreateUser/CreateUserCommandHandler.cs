using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.UserFeatures.Commands.CreateUser;
public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserCommandResponse>
{
    private readonly IUserService _userService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateUserCommandHandler(ILocalizationService localizationService, IUserService userService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _userService = userService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _userService.AddAsync(companyId, request.User);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("UserCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
