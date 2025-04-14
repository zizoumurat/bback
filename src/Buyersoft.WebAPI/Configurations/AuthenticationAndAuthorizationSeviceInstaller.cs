using Microsoft.AspNetCore.Authentication.JwtBearer;
using Buyersoft.WebApi.OptionsSetup;
using Buyersoft.WebAPI.OptionsSetup;

namespace Buyersoft.WebApi.Configurations;

public class AuthenticationAndAuthorizationSeviceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.ConfigureOptions<EmailOptionsSetup>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        services.AddAuthorization(options =>
        {
            AuthorizationPolicyGenerator.AddDynamicPolicies(options);
        });
    }
}
