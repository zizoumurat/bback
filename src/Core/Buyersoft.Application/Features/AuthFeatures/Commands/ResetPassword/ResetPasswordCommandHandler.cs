using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.AppFeatures.AuthFeatures.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, ResetPasswordCommandResponse>
    {
        private readonly IAuthService _authService;
        private readonly ILocalizationService _localizationService;

        public ResetPasswordCommandHandler(IAuthService authService, ILocalizationService localizationService)
        {
            _authService = authService;
            _localizationService = localizationService;
        }

        public async Task<ResetPasswordCommandResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {

            await _authService.ResetPasswordAsync(request.token, request.newPassword);

            return new(_localizationService.GetLocalizedString("PasswordResetSuccessMessage"));
        }
    }
}
