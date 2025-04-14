using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Enums;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IReverseAuctionService
{
    Task AddAsync(int companyId, AddReverseAuctionDto model);

    Task ChangeStatu(int id, ReverseAuctionStatusEnum statu, int remainingSeconds);

    Task<PaginatedList<ReverseAuctionListDto>> GetAllAsync(int companyId, ReverseAuctionFilterDto filter, PageRequest pagination);

    Task<ReverseAuctionListDto> GetById(int Id);
}
