using Buyersoft.Application.Services;
using Buyersoft.Domain.Repositories.DepartmentRepositories;
using Buyersoft.Domain.Repositories.Generic;
using Buyersoft.Domain.UnitOfWorks;
using Buyersoft.Persistance.Repositories.DepartmentRepositories;
using Buyersoft.Persistance.Repositories.Generic;
using Buyersoft.Persistance.Services;
using Buyersoft.Persistance.UnitOfWorks;
//UsingSpot

namespace Buyersoft.WebApi.Configurations;

public class PersistanceDIServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        #region Context UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITransactionManager, TransactionManager>();
        #endregion

        #region Services
        services.AddScoped<IAuthService, AuthService>();

        #endregion

        #region Repositories
        services.AddScoped(typeof(IAddRepository<>), typeof(AddRepository<>));
        services.AddScoped(typeof(IUpdateRepository<>), typeof(UpdateRepository<>));
        services.AddScoped(typeof(IDeleteRepository<>), typeof(DeleteRepository<>));
        services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));

        #endregion
    }
}
