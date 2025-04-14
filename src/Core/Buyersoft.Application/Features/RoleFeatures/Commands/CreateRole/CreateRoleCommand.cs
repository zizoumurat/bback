using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RoleFeatures.Commands.CreateRole;

public sealed record CreateRoleCommand(RoleDto Role) : ICommand<CreateRoleCommandResponse>;