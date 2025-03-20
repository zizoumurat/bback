using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.SupplierActionFeatures.Commands.UpdateSupplierAction;

public sealed record UpdateSupplierActionCommand(SupplierActionUpdateStatusDto SupplierAction) : ICommand<UpdateSupplierActionCommandResponse>;