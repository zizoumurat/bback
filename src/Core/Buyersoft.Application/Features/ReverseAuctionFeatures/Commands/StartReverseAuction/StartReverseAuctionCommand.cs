using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Enums;

namespace Buyersoft.Application.Features.ReverseAuctionFeatures.Commands.StartReverseAuction;

public sealed record StartReverseAuctionCommand(int Id, ReverseAuctionStatusEnum Statu, int remainingSeconds) : ICommand<StartReverseAuctionCommandResponse>;