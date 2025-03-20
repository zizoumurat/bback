using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.AuthFeatures.Commands.GetTokenByRefreshToken;

public sealed record GetTokenByRefreshTokenCommand(
    string UserId,
    string RefreshToken,
    string CompanyId
    ): ICommand<GetTokenByRefreshTokenCommandResponse>;
