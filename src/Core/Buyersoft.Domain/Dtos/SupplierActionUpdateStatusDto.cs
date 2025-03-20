using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Dtos;

public sealed record SupplierActionUpdateStatusDto(
    int Id,
    string SupplierNotes,
    SupplierActionStatusEnum SupplierActionStatus
 );

