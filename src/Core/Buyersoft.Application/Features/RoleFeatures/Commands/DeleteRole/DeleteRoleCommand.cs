using Buyersoft.Application.Features.RoleFeatures.Commands.DeleteRole;
using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.RoleFeatures.Commands.DeleteRole;
public sealed record DeleteRoleCommand(int Id) : ICommand<DeleteRoleCommandResponse>;
