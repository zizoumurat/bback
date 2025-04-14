using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.BankInfoFeatures.Queries.GetAllBankInfos;

public sealed record GetAllBankInfosQueryResponse(PaginatedList<BankInfoListDto> result);
