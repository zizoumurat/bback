using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.Generic;

namespace Buyersoft.Domain.Repositories.SupplierRepositories;

public interface IQuerySupplierRepository : IQueryRepository<Supplier>
{
    Task<List<SupplierDtoForCategory>> GetListForCategory(int companyId, int requestGroupId, int? cityId, int channelType);
}