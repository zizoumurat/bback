using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.UserFeatures.Commands.UpdateProfile;
public class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileCommand, UpdateProfileCommandResponse>
{
    private readonly IUserService _userService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public UpdateProfileCommandHandler(ILocalizationService localizationService, IUserService userService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _userService = userService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<UpdateProfileCommandResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int userId = _tokenService.GetUserIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _userService.UpdateProfileAsync(userId, request.User);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("UserUpdated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
