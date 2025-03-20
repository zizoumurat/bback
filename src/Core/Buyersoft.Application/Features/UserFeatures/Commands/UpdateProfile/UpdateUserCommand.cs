using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.UserFeatures.Commands.UpdateProfile;

public sealed record UpdateProfileCommand(UpdateProfileDto User) : ICommand<UpdateProfileCommandResponse>;