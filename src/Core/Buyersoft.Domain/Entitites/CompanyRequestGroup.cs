using Buyersoft.Domain.Entitites.Base;

namespace Buyersoft.Domain.Entitites;

public class CompanyRequestGroup : SelectableEntity
{
    public int RequestGroupId { get; set; }
    public virtual RequestGroup RequestGroup { get; set; }

    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }
}