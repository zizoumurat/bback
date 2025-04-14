using Microsoft.EntityFrameworkCore;
using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Persistance;
using Buyersoft.Persistance.Context;
using Microsoft.AspNetCore.Identity;

namespace Buyersoft.WebApi.Configurations;

public class PersistanceServiceInstaller : IServiceInstaller
{
    private const string SectionName = "ConnectionString";
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString(SectionName);

        services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(connectionString));

        services.AddIdentity<User, Role>(options =>
        {
            options.Tokens.EmailConfirmationTokenProvider = "Default";
        })
            .AddEntityFrameworkStores<BaseDbContext>()
            .AddDefaultTokenProviders(); 

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(1); 
        });


        services.AddAutoMapper(typeof(AssemblyReference).Assembly);
    }
}
