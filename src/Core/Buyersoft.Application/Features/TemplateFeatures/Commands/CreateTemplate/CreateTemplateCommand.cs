using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.TemplateFeatures.Commands.CreateTemplate;

public sealed record CreateTemplateCommand(TemplateDto Template) : ICommand<CreateTemplateCommandResponse>;