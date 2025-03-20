using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.BrancheFeatures.Queries.GetComments;

public sealed record GetCommentsQueryResponse(List<CommentListDto> result);
