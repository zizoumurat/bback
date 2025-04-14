using Buyersoft.Application.Features.TemplateFeatures.Commands.DeleteTemplate;
using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.TemplateFeatures.Commands.DeleteTemplate;
public sealed record DeleteTemplateCommand(int Id) : ICommand<DeleteTemplateCommandResponse>;
