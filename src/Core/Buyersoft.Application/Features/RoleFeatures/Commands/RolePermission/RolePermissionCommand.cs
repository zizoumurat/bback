using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RoleFeatures.Commands.RolePermission;

public sealed record RolePermissionCommand(UpdateRolePermissionDto RolePermission) : ICommand<RolePermissionCommandResponse>;