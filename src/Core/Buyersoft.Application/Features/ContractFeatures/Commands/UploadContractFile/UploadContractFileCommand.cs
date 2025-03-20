using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.ContractFeatures.Commands.UploadContractFile;

public sealed record UploadContractFileCommand(UploadContractFileDto Model) : ICommand<UploadContractFileCommandResponse>;