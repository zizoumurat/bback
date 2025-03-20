using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IOfferService
{
    Task RejectOfferAsync(int companyId, RejectOfferDto model);
    Task MakeOfferAsync(int companyId, MakeOfferDto model);
    Task UpdateOfferPrices(int companyId, List<UpdateOfferPriceDto> Model);
    Task RequestRevision(int companyId, int OfferId);
    Task<List<OfferListDto>> GetOfferListByRequest(int companyId, int requestId);
    Task AddToShortList(int companyId, int offerId);
    Task RemoveToShortList(int companyId, int offerId);
    Task AddToFavorite(int companyId, int offerId);
    Task RemoveToFavorite(int companyId, int offerId);

    Task SetAllocation(int companyId, int RequestId, List<OfferDetailDto> OfferDetailList);
    Task<PaginatedList<RequestListDto>> GetAllAsync(int companyId, RequestFilterDto filter, PageRequest pagination);
}
