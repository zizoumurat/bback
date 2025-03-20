using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.AppFeatures.AuthFeatures.Commands.ForgotPassword
{
    public sealed record ForgotPasswordCommand(string email) : ICommand<ForgotPasswordCommandResponse>;
}
