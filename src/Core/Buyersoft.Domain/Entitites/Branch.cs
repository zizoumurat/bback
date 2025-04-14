namespace Buyersoft.Domain.Entitites;

public class Branch : BaseEntity
{
    public string BankName { get; set; }
    public string BranchName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string FaksNumber { get; set; }
    public int CityId { get; set; }
    public int DistrictId { get; set; }

    public virtual City City { get; set; }
    public virtual District District { get; set; }
    public virtual ICollection<BankInfo> BankInfos { get; set; }
}
