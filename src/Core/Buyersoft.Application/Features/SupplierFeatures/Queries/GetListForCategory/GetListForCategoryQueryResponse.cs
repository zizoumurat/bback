using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.BrancheFeatures.Queries.GetListForCategory;

public sealed record GetListForCategoryQueryResponse(IList<SupplierDtoForCategory> result);
