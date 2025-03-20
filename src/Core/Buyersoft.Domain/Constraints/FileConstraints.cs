namespace Buyersoft.Domain.Constraints;
public static class FileConstraints
{
    public const long MaxLogoFileSize = 2 * 1024 * 1024;
    public const long MaxDocumentFileSize = 2 * 1024 * 1024;
    public static readonly string[] AllowedLogoFileMimeTypes = { "image/png", "image/jpeg" };
    public static readonly string[] AllowedDocumentFileMimeTypes =
    {
        "image/png",
        "image/jpeg",
        "application/pdf",
        "application/vnd.openxmlformats-officedocument.wordprocessingml.document", // DOCX
        "application/vnd.ms-excel", // XLS
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" // XLSX
    };
}