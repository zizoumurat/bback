using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.UserFeatures.Commands.CreateUser;

public sealed record CreateUserCommand(UserCreateDto User) : ICommand<CreateUserCommandResponse>;