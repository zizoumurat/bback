using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.AppFeatures.AuthFeatures.Commands.ResetPassword
{
    public sealed record ResetPasswordCommand(string token, string newPassword) : ICommand<ResetPasswordCommandResponse>;
}
