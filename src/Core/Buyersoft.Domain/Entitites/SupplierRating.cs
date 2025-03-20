using Buyersoft.Domain.Entitites.Identity;

namespace Buyersoft.Domain.Entitites;

public class SupplierRating : BaseEntity
{
    public int SupplierId { get; set; }
    public int CompanyId { get; set; }
    public int UserId { get; set; }
    public int QualityScore { get; set; } 
    public int CertificationScore { get; set; }
    public int HealthAndSafetyScore { get; set; } 
    public int EnvironmentalComplianceScore { get; set; } 
    public int EthicsAndCommercialTermsScore { get; set; } 
    public int ActionClosurePerformanceScore { get; set; } 

    public virtual Supplier Supplier { get; set; }
    public virtual Company Company { get; set; }
    public virtual User User { get; set; }
}
