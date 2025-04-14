using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.ServiceDefinitionFeatures.Queries.GetAllServiceDefinitions;
public sealed record GetAllServiceDefinitionsQuery(ServiceDefinitionDto filter, PageRequest pagination) : IQuery<GetAllServiceDefinitionsQueryResponse>;
