namespace Buyersoft.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);

    Task SendResetPasswordEmailAsync(string to, string token);
}