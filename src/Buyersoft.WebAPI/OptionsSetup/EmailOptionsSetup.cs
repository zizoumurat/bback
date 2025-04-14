using Microsoft.Extensions.Options;
using Buyersoft.Infrastructure.Email;

namespace Buyersoft.WebApi.OptionsSetup;

public sealed class EmailOptionsSetup : IConfigureOptions<EmailOptions>
{
    private const string EmailSettings = nameof(EmailSettings);
    private readonly IConfiguration _configuration;

    public EmailOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(EmailOptions options)
    {
        _configuration.GetSection(EmailSettings).Bind(options);
    }
}
