using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Dtos;

public sealed record SupplierActionCreateDto(
    int SupplierId,
    NonconformityReasonEnum Type,
    string Subject,
    string Detail,
    DateTime DueDate
 );

