using Buyersoft.Application.Abstractions;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Entitites.Identity;
using Microsoft.AspNetCore.Identity;

namespace Buyersoft.Application.Features.AppFeatures.AuthFeatures.Commands.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;
        public LoginCommandHandler(IJwtProvider jwtProvider, UserManager<User> userManager, IAuthService authService)
        {
            _jwtProvider = jwtProvider;
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User user = await _authService.GetByEmailAsync(request.email);

            if (user == null) throw new InvalidOperationException("Kullanıcı bulunamadı!");

            var checkUser = await _authService.CheckPasswordAsync(user, request.password);
            if (!checkUser) throw new InvalidOperationException("Şifreniz yanlış!");

            var accessToken = await _jwtProvider.CreateTokenAsync(user);
            LoginCommandResponse response = new(
                Token: accessToken.Token,
                RefreshToken: accessToken.RefreshToken,
                RefreshTokenExpires: accessToken.RefreshTokenExpires
                );

            return response;
        }
    }
}
