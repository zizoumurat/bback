using Microsoft.AspNetCore.Http;
using Buyersoft.Application.Services;
using System.IdentityModel.Tokens.Jwt;

namespace Buyersoft.Infrastructure.Services;

public sealed class TokenService : ITokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetUserIdByToken()
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Contains("authentication"))?.Value;

        var userName =_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == JwtRegisteredClaimNames.Sub)?.Value;

        if (int.TryParse(userIdClaim, out int userId))
        {
            return userId;
        }

        return 0;
    }

    public string GetUserNameByToken()
    {
        var userName = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == JwtRegisteredClaimNames.Sub)?.Value;


        return userName;
    }

    public int GetCompanyIdByToken()
    {
        var companyIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Contains("CompanyID"))?.Value;

        if (int.TryParse(companyIdClaim, out int companyId))
        {
            return companyId;
        }

        return 0;
    }

    public int GetSupplierIdByToken()
    {
        var supplierIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Contains("SupplierId"))?.Value;

        if (int.TryParse(supplierIdClaim, out int supplierId))
        {
            return supplierId;
        }

        return 0;
    }
}
