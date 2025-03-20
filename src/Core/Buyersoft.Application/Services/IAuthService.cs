using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites.Identity;

namespace Buyersoft.Application.Services;
public interface IAuthService
{
    Task<User> GetByEmailAsync(string emailOrUserName);
    Task<User> GetById(int id);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task ForgotPasswordAsync(string email);
    Task ResetPasswordAsync(string token, string newPassword);
    Task ChangePassword(int UserId, UpdatePasswordDto Model);
    Task UpdateProfile(int UserId, UpdateProfileDto Model);
}
