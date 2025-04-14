using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.PaymentListFeatures.Queries.GetAllPaymentLists;
public sealed record GetAllPaymentListsQuery(PaymentListFilterDto filter, PageRequest pagination) : IQuery<GetAllPaymentListsQueryResponse>;
