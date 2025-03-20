using Buyersoft.Application.Abstractions;
using Buyersoft.Application.Services;
using Buyersoft.Infrastructure.Authentication;
using Buyersoft.Infrastructure.Services;

namespace Buyersoft.WebApi.Configurations;

public class InfrustructureDIServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IJwtProvider,JwtProvider>();
        services.AddScoped<ITokenService,TokenService>();
        services.AddScoped<ILocalizationService,LocalizationService>();
        services.AddScoped<IEmailService,EmailService>();
    }
}
