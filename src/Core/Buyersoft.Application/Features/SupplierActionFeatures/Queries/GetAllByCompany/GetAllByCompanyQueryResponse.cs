using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.SupplierActionFeatures.Queries.GetAllByCompany;

public sealed record GetAllByCompanyQueryResponse(IList<SupplierActionListDto> result);
