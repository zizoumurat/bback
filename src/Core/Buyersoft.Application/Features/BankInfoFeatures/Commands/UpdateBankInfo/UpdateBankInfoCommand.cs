using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.BankInfoFeatures.Commands.UpdateBankInfo;

public sealed record UpdateBankInfoCommand(BankInfoDto BankInfo) : ICommand<UpdateBankInfoCommandResponse>;