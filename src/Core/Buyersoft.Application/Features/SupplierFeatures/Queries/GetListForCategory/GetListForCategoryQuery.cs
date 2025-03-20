using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.BrancheFeatures.Queries.GetListForCategory;

public sealed record GetListForCategoryQuery(int requestGroupId, int? cityId, int channelType) : IQuery<GetListForCategoryQueryResponse>;
