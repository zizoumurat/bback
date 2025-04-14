using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.BankInfoFeatures.Queries.GetAllBankInfos;

public sealed record GetAllBankInfosQuery
    (BankInfoFilterDto filter, PageRequest pagination) : IQuery<GetAllBankInfosQueryResponse>;
