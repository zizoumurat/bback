using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.SupplierActionFeatures.Queries.GetAllByCompany;
public sealed record GetAllByCompanyQuery(int SupplierId) : IQuery<GetAllByCompanyQueryResponse>;
