using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Buyersoft.Application.Abstractions;
using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Buyersoft.Infrastructure.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<User> _userManager;

        public JwtProvider(IOptions<JwtOptions> jwtOptions, UserManager<User> userManager)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
        }


        public async Task<TokenRefreshTokenDto> CreateTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,$"{user.Name} {user.Surname}"),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Authentication, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim("CompanyID", user.CompanyId.ToString()),
                new Claim("RoleId", user.Role.Id.ToString()),
                new Claim("SupplierId", user?.Company?.Supplier?.Id.ToString() ?? "0"),
            };

            foreach (var permission in user.Role.RolePermissions)
            {
                claims.Add(new Claim("Permission", permission.Permission.Name));
            }


            DateTime expires = DateTime.Now.AddDays(1); 

            JwtSecurityToken jwtSecurityToken = new(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256));

            string token = new JwtSecurityTokenHandler()
                 .WriteToken(jwtSecurityToken);


            string refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = expires.AddDays(1);
            await _userManager.UpdateAsync(user);

            return new(token,refreshToken,user.RefreshTokenExpires);
        }
    }
}
