using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.DepartmentFeatures.Commands.CreateDepartment;

public sealed record CreateDepartmentCommand(DepartmentDto Department) : ICommand<CreateDepartmentCommandResponse>;