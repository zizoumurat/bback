using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.AppFeatures.AuthFeatures.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand, ForgotPasswordCommandResponse>
    {
        private readonly IAuthService _authService;
        private readonly ILocalizationService _localizationService;

        public ForgotPasswordCommandHandler(IAuthService authService, ILocalizationService localizationService)
        {
            _authService = authService;
            _localizationService = localizationService;
        }

        public async Task<ForgotPasswordCommandResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {

            await _authService.ForgotPasswordAsync(request.email);

            return new(_localizationService.GetLocalizedString("PasswordResetLinkSentMessage"));
        }
    }
}
