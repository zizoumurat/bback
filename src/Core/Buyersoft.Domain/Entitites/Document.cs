using Buyersoft.Domain.Entitites.Identity;

namespace Buyersoft.Domain.Entitites;

public class Document : BaseEntity
{
    public string FileName { get; set; }
    public byte[] FileContent { get; set; }
    public string FileType { get; set; }
    public DateTime UploadDate { get; set; }
    public long FileSize { get; set; }

    public virtual ICollection<RequestDocument> RequestDocuments { get; set; }
    public virtual Company Company { get; set; }
    public virtual User User { get; set; }
}