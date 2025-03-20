    using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace Buyersoft.Application.Features.CompanyFeatures.Commands.UpdateCompany;

public sealed record UpdateCompanyCommand(UpdateCompanyDto Company) : ICommand<UpdateCompanyCommandResponse>;