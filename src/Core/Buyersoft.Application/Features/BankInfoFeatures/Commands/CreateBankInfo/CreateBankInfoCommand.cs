using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.BankInfoFeatures.Commands.CreateBankInfo;

public sealed record CreateBankInfoCommand(BankInfoDto BankInfo) : ICommand<CreateBankInfoCommandResponse>;
