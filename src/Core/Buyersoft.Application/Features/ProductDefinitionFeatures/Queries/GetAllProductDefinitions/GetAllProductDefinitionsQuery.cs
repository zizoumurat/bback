using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.ProductDefinitionFeatures.Queries.GetAllProductDefinitions;
public sealed record GetAllProductDefinitionsQuery(ProductDefinitionDto filter, PageRequest pagination) : IQuery<GetAllProductDefinitionsQueryResponse>;
