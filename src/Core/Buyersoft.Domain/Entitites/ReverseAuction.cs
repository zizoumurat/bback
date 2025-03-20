using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Entitites;

public class ReverseAuction : BaseEntity
{
    public int RequestId { get; set; }
    public string MeetLink { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Minutes { get; set; }
    public ReverseAuctionStatusEnum Statu { get; set; }
    public bool ShowCompanyNames { get; set; }
    public bool ShowAllOffers { get; set; }
    public bool ShowOfferRankings { get; set; }

    public Request Request { get; set; }
}