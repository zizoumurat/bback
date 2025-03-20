using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Abstractions;

public interface IJwtProvider
{
    Task<TokenRefreshTokenDto> CreateTokenAsync(User user);
}
