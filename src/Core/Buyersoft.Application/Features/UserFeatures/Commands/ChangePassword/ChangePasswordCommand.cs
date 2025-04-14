using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.UserFeatures.Commands.ChangePassword;

public sealed record ChangePasswordCommand(UpdatePasswordDto User) : ICommand<ChangePasswordCommandResponse>;