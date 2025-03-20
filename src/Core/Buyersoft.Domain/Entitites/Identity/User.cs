using Microsoft.AspNetCore.Identity;

namespace Buyersoft.Domain.Entitites.Identity;
public class User : IdentityUser<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpires { get; set; }
    public int RoleId { get; set; }
    public int CompanyId { get; set; }
    public int DepartmentId { get; set; }
    public string Title { get; set; }
    public int? UserPhotoId { get; set; }
    public string ChoosenLanguage { get; set; }

    public virtual Company Company { get; set; }
    public virtual Department Department { get; set; }
    public virtual Role Role { get; set; }
    public virtual Document UserPhoto { get; set; }
    public virtual ICollection<ApprovalChainUser> ApprovalChainUsers { get; set; }
    public virtual ICollection<Notification> Notifications { get; set; }
    public virtual ICollection<UserNotificationPreference> NotificationPreferences { get; set; }
    public virtual ICollection<Request> CreatedRequests { get; set; }
    public virtual ICollection<Request> ManagedRequests { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<CategoryUser> CategoryUsers { get; set; }
    public virtual ICollection<ContractComment> ContractComments { get; set; }
    public virtual ICollection<SupplierAction> SupplierActions { get; set; }
}