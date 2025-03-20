using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Dtos;

public sealed record SupplierActionCreateDto(
    int SupplierId,
    SupplierActionTypeEnum Type,
    string Subject,
    string Detail,
    DateTime DueDate
 );

