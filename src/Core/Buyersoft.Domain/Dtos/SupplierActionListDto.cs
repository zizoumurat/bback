using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Dtos;

public sealed record SupplierActionListDto(
    int Id,
    SupplierActionTypeEnum Type,
    string Subject,
    string SupplierNotes,
    string Detail,
    DateTime DueDate,
    SupplierActionStatusEnum SupplierActionStatus
 );

