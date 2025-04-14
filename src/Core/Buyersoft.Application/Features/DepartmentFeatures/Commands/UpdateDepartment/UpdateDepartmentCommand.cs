using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.DepartmentFeatures.Commands.UpdateDepartment;

public sealed record UpdateDepartmentCommand(DepartmentDto Department) : ICommand<UpdateDepartmentCommandResponse>;