using Buyersoft.Application.Services;
using Buyersoft.Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Buyersoft.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly IOptions<EmailOptions> _emailOptions;
    private readonly IConfiguration _configuration;
    public EmailService(IOptions<EmailOptions> emailOptions, IConfiguration configuration)
    {
        _emailOptions = emailOptions;
        _smtpClient = new SmtpClient(emailOptions.Value.SmtpServer)
        {
            Port = emailOptions.Value.Port,
            Credentials = new NetworkCredential(emailOptions.Value.Username, emailOptions.Value.Password),
            EnableSsl = true,
        };
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var mailMessage = new MailMessage(_emailOptions.Value.From, to, subject, body)
        {
            IsBodyHtml = true
        };

        await _smtpClient.SendMailAsync(mailMessage);
    }

    public async Task SendResetPasswordEmailAsync(string to, string token)
    {
        var appUrl = _configuration.GetSection("AppUrl").Value;
        var resetLink = $"{appUrl}#/auth/reset-password?token={token}";
        var logoUrl = $"{appUrl}assets/layout/images/logo-dark.png";

        var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "resetpassword.html");
        var emailBody = await File.ReadAllTextAsync(templatePath);

        emailBody = emailBody.Replace("{{appUrl}}", appUrl);
        emailBody = emailBody.Replace("{{resetLink}}", resetLink);
        emailBody = emailBody.Replace("{{logoUrl}}", logoUrl);

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailOptions.Value.From),
            Subject = "Şifre Sıfırlama Talebi",
            Body = emailBody,
            IsBodyHtml = true
        };

        mailMessage.To.Add(to);

        await _smtpClient.SendMailAsync(mailMessage);
    }
}
