using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.PaymentListFeatures.Queries.GetAllPaymentLists;

public sealed record GetAllPaymentListsQueryResponse(PaginatedList<PaymentListDto> result);
