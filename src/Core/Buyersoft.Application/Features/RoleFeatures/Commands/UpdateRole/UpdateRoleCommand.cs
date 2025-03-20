using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RoleFeatures.Commands.UpdateRole;

public sealed record UpdateRoleCommand(RoleDto Role) : ICommand<UpdateRoleCommandResponse>;