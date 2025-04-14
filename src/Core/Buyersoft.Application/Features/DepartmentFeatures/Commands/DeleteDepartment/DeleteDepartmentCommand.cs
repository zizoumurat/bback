using Buyersoft.Application.Features.DepartmentFeatures.Commands.DeleteDepartment;
using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.DepartmentFeatures.Commands.DeleteDepartment;
public sealed record DeleteDepartmentCommand(int Id) : ICommand<DeleteDepartmentCommandResponse>;
