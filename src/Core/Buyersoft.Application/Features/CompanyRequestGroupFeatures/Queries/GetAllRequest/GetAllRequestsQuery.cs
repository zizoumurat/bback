using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.CompanyRequestGroupFeatures.Queries.GetAllCompanyRequestGroups;
public sealed record GetAllCompanyRequestGroupsQuery(int SubCategoryId) : IQuery<GetAllCompanyRequestGroupsQueryResponse>;
