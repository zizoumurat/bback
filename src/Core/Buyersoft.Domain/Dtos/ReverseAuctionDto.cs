using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Dtos;

public sealed record ReverseAuctionDto(
    int Id,
    int RequestId,
    string MeetLink,
    DateTime StartTime,
    DateTime EndTime,
    string RequestCode,
    bool ShowCompanyNames,
    bool ShowAllOffers,
    bool ShowOfferRankings,
    ReverseAuctionStatusEnum Statu
 );

public sealed record AddReverseAuctionDto(
    int RequestId,
    DateTime StartTime,
    DateTime EndTime,
    string MeetLink,
    bool ShowCompanyNames,
    bool ShowAllOffers,
    bool ShowOfferRankings,
    List<int> OfferIdList
 );

public sealed class ReverseAuctionListDto
{
    public int Id { get; init; }
    public int RequestId { get; init; }
    public string MeetLink { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public string RequestCode { get; init; }
    public string RequestTitle { get; init; }
    public bool ShowCompanyNames { get; init; }
    public bool ShowAllOffers { get; init; }
    public bool ShowOfferRankings { get; init; }
    public DateTime[] Times { get; init; }
    public Participant[] ParticipantList { get; set; }
    public int? Minutes { get; set; }
    public ReverseAuctionStatusEnum Statu { get; set; }
}

public class Participant
{
    public string Name { get; set; }
    public ParticipationStatusEnum Status { get; set; }
}

public sealed record ReverseAuctionFilterDto(
    int RequestId,
    DateTime StartTime,
    DateTime EndTime
);