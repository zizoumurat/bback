namespace Buyersoft.Domain.Dtos;
public record CurrencyParameterDto(
    int Id,
    int CompanyId,
    int Currency1Id,
    int Currency2Id,
    decimal ExchangeRate,
    DateTime StartDate,
    DateTime ExpiredDate
 );

public sealed record CurrencyParameterFilterDto(
     int Currency1Id,
     int Currency2Id,
     int CompanyId,
     decimal ExchangeRate,
     DateTime? ExpiredDate,
     DateTime? StartDate
);

public class CurrencyParameterListDto()
{
    public int Id { get; set; }
    public int Currency1Id { get; set; }
    public string Currency1Name { get; set; }
    public string Currency1Code { get; set; }
    public string Currency2Name { get; set; }
    public string Currency2Code { get; set; }
    public int Currency2Id { get; set; }
    public int CompanyId { get; set; }
    public decimal ExchangeRate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpiredDate { get; set; }
}

public sealed record ExchangeRateDto(string Name, decimal ExchangeRate);
