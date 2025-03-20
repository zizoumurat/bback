using Buyersoft.Application.Messaging;
using Microsoft.AspNetCore.Http;

namespace Buyersoft.Application.Features.CompanyFeatures.Commands.UpdateCompanyLogo;

public sealed record UpdateCompanyLogoCommand(IFormFile File) : ICommand<UpdateCompanyLogoCommandResponse>;