using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.SupplierActionFeatures.Queries.GetAllBySupplier;
public sealed record GetAllBySupplierQuery(int CompanyId) : IQuery<GetAllBySupplierQueryResponse>;
