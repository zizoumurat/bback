using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.AuthFeatures.Commands.GetTokenByRefreshToken;

public sealed record GetTokenByRefreshTokenCommandResponse(
    TokenRefreshTokenDto Token,
    string Email,
    int UserId,
    string NameLastName,
    int Year);
