using Buyersoft.Application.Features.BankInfoFeatures.Commands.DeleteBankInfo;
using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.BankInfoFeatures.Commands.DeleteBankInfo;
public sealed record DeleteBankInfoCommand(int Id) : ICommand<DeleteBankInfoCommandResponse>;
