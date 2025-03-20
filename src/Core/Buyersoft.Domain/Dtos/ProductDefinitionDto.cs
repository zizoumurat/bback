namespace Buyersoft.Domain.Dtos;

public sealed record ProductDefinitionDto(
    int Id,
    string Code,
    string Definition,
    int CategoryId
 );

public sealed record ServiceDefinitionDto(
    int Id,
    string Definition,
    int CategoryId
 );
