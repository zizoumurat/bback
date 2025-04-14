using Buyersoft.Domain.Dtos;


namespace Buyersoft.Application.Features.AppFeatures.AuthFeatures.Commands.Login
{
    public sealed record LoginCommandResponse(string Token, string RefreshToken, DateTime RefreshTokenExpires);
        
}
