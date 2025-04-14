using Microsoft.Extensions.Options;
using Buyersoft.Infrastructure.Authentication;

namespace Buyersoft.WebApi.OptionsSetup;

public sealed class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private const string Jwt = nameof(Jwt);
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(Jwt).Bind(options);
    }
}
