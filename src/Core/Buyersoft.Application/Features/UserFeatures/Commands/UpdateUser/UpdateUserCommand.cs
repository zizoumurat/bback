using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.UserFeatures.Commands.UpdateUser;

public sealed record UpdateUserCommand(UserUpdateDto User) : ICommand<UpdateUserCommandResponse>;