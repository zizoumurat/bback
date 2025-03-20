namespace Buyersoft.Domain.Enums;

public enum RequestStateEnum 
{
    NotStarted = 0,
    Started = 1,
    ComparisonTableCreated = 2,
    ReverseAuctionPending = 3,
    ReverseAuctionComplated = 4,
    AllocationCreated = 5,
    ComparisonTableCompleted = 6,
    PendingApprovals = 7,
    Approved = 8,
    Rejected = 9,
    Cancelled = 10,
    Completed = 11
}
