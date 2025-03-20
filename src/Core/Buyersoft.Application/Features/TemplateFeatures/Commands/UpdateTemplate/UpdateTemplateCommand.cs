using Buyersoft.Application.Features.TemplateFeatures.Commands.CreateTemplate;
using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.TemplateFeatures.Commands.UpdateTemplate;

public sealed record UpdateTemplateCommand(TemplateDto Template) : ICommand<UpdateTemplateCommandResponse>;