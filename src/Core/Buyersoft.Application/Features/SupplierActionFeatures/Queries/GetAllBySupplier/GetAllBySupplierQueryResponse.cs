using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.SupplierActionFeatures.Queries.GetAllBySupplier;

public sealed record GetAllBySupplierQueryResponse(IList<SupplierActionListDto> result);
