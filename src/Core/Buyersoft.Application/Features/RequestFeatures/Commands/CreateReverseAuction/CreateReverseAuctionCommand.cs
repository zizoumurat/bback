using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.CreateReverseAuction;

public sealed record CreateReverseAuctionCommand(AddReverseAuctionDto Model) : ICommand<CreateReverseAuctionCommandResponse>;