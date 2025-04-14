namespace Buyersoft.Domain.Dtos;

public sealed record DocumentDto(int Id, string FileName, byte[] FileContent, string FileType, long FileSize);
