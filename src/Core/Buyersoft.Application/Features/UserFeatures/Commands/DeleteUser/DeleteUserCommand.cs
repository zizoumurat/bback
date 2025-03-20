using Buyersoft.Application.Features.UserFeatures.Commands.DeleteUser;
using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.UserFeatures.Commands.DeleteUser;
public sealed record DeleteUserCommand(int Id) : ICommand<DeleteUserCommandResponse>;
