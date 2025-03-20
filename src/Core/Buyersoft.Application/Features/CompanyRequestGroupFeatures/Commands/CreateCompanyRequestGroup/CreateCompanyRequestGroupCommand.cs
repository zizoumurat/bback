﻿using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.CompanyRequestGroupFeatures.Commands.CreateCompanyRequestGroup;

public sealed record CreateCompanyRequestGroupCommand(CompanyRequestGroupDto CompanyRequestGroup) : ICommand<CreateCompanyRequestGroupCommandResponse>;