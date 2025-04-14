using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.AppFeatures.AuthFeatures.Commands.Login
{
    public sealed record LoginCommand(
        string email,
        string password) : ICommand<LoginCommandResponse>;
}
