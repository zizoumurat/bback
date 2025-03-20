using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Buyersoft.Persistance.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly ILocalizationService _localizationService;
    private readonly EncryptionHelper _encryptionHelper;

    public AuthService(UserManager<User> userManager, IEmailService emailService, ILocalizationService localizationService, EncryptionHelper encryptionHelper)
    {
        _userManager = userManager;
        _emailService = emailService;
        _localizationService = localizationService;
        _encryptionHelper = encryptionHelper;
    }

    public async Task ChangePassword(int UserId, UpdatePasswordDto Model)
    {
        var user = await _userManager.FindByIdAsync(UserId.ToString()) ?? throw new InvalidOperationException(_localizationService.GetLocalizedString("UserNotFoundByEmail"));

        await _userManager.ChangePasswordAsync(user, Model.Password, Model.NewPassword);
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task ForgotPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new InvalidOperationException(_localizationService.GetLocalizedString("UserNotFoundByEmail"));
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        string data = $"{user.Id}|{token}";
        string encryptedData = _encryptionHelper.EncryptData(data);

        var _ = _emailService.SendResetPasswordEmailAsync(email, encryptedData);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _userManager.Users
            .Include(x => x.Company)
                .ThenInclude(x => x.Supplier)
            .Include(x => x.Role)
                .ThenInclude(x => x.RolePermissions)
                    .ThenInclude(x => x.Permission).
             Where(p => p.Email == email).FirstOrDefaultAsync();
    }

    public async Task<User> GetById(int id)
    {
        return await _userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task ResetPasswordAsync(string token, string newPassword)
    {

        string decryptedData = _encryptionHelper.DecryptData(token);

        var parts = decryptedData.Split('|');
        var userId = parts[0];
        string resetToken = parts[1];

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("UserNotFoundByEmail"));
        }


        var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
    }

    public async Task UpdateProfile(int UserId, UpdateProfileDto Model)
    {
        var user = await _userManager.FindByIdAsync(UserId.ToString()) ?? throw new InvalidOperationException(_localizationService.GetLocalizedString("UserNotFoundByEmail"));

        user.Name = Model.Name;
        user.Surname = Model.Surname;
        user.PhoneNumber = Model.PhoneNumber;
        user.Title = Model.Title;
        user.ChoosenLanguage = Model.ChoosenLanguage;
        user.Email = Model.Email;

        await _userManager.UpdateAsync(user);
    }
}
