using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.SupplierActionFeatures.Commands.CreateSupplierAction;

public sealed record CreateSupplierActionCommand(SupplierActionCreateDto SupplierAction) : ICommand<CreateSupplierActionCommandResponse>;