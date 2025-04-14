using Buyersoft.Application.Abstractions;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Entitites.Identity;
using Microsoft.AspNetCore.Identity;

namespace Buyersoft.Application.Features.AuthFeatures.Commands.GetTokenByRefreshToken;

public sealed class GetTokenByRefreshTokenCommandHandler : ICommandHandler<GetTokenByRefreshTokenCommand, GetTokenByRefreshTokenCommandResponse>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly UserManager<User> _userManager;
    private readonly IAuthService _authService;
    public GetTokenByRefreshTokenCommandHandler(IJwtProvider jwtProvider, UserManager<User> userManager, IAuthService authService)
    {
        _jwtProvider = jwtProvider;
        _userManager = userManager;
        _authService = authService;
    }

    public async Task<GetTokenByRefreshTokenCommandResponse> Handle(GetTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null) throw new Exception("Kullanıcı bulunamadı!");

        if (user.RefreshToken != request.RefreshToken)
            throw new Exception("Refresh Token geçerli değil!");

        GetTokenByRefreshTokenCommandResponse response = new(
            Token: await _jwtProvider.CreateTokenAsync(user),
            Email: user.Email,
            UserId: user.Id,
            NameLastName: $"{user.Name} {user.Surname}",
            Year: DateTime.Now.Year
            );

        return response;
    }
}
