using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;
using System.Text;

namespace Buyersoft.Domain.Helpers;
public class EncryptionHelper
{
    private readonly IDataProtector _protector;

    public EncryptionHelper(IDataProtectionProvider provider)
    {
        _protector = provider.CreateProtector("ResetPasswordPurpose");
    }

    public string EncryptData(string plainText)
    {
        return _protector.Protect(plainText);
    }

    public string DecryptData(string cipherText)
    {
        try
        {
            return _protector.Unprotect(cipherText);
        }
        catch
        {
            return null;
        }
    }
}

