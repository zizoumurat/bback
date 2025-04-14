using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.SupplierRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Repositories.SupplierRepositories;

public class QuerySupplierRepository : QueryRepository<Supplier>, IQuerySupplierRepository
{
    public QuerySupplierRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<List<SupplierDtoForCategory>> GetListForCategory(int companyId, int requestGroupId, int? cityId, int channelId)
    {
        var suppliers = await _context.Suppliers
        .Where(s => s.SupplierRequestGroups.Any(sc => sc.RequestGroup.CompanyRequestGroup.Id == requestGroupId) && (cityId == default || s.Company.CityId == cityId))
        .Where(x => channelId == 2 || x.Company.SupplierPortfolios.Any(xx => xx.CompanyId == companyId))
        .Select(s => new SupplierDtoForCategory
        {
            Id = s.CompanyId,
            Name = s.Company.Name,
            BuyersoftRating = 3,
            UserRating = 4,
            Email = s.Company.Email,
            WebSite = "---",
            Channel = s.Company.SupplierPortfolios.Any(cs => cs.CompanyId == companyId) ? 1 : 2
        })
        .ToListAsync();


        return suppliers;
    }
}
