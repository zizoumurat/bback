using AutoMapper;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Constraints;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.DocumentRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace Buyersoft.Persistance.Services;
public class DocumentService : IDocumentService
{
    private readonly IAddDocumentRepository _addDocumentRepository;
    private readonly IUpdateDocumentRepository _updateDocumentRepository;
    private readonly IDeleteDocumentRepository _deleteDocumentRepository;
    private readonly IQueryDocumentRepository _queryDocumentRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public DocumentService(IAddDocumentRepository addDocumentRepository,
        IUpdateDocumentRepository updateDocumentRepository,
        IDeleteDocumentRepository deleteDocumentRepository,
        IQueryDocumentRepository queryDocumentRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addDocumentRepository = addDocumentRepository;
        _updateDocumentRepository = updateDocumentRepository;
        _deleteDocumentRepository = deleteDocumentRepository;
        _queryDocumentRepository = queryDocumentRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<int> AddAsync(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var fileContent = memoryStream.ToArray();

        Document document = new()
        {
            UploadDate = DateTime.Now,
            FileContent = fileContent,
            FileName = file.FileName,
            FileSize = file.Length,
            FileType = file.ContentType
        };

        await _addDocumentRepository.AddAsync(document);

        return document.Id;
    }

    public async Task UpdateAsync(IFormFile file, int Id)
    {
        bool exists = await _queryDocumentRepository.IsExisting(x => x.Id == Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var fileContent = memoryStream.ToArray();

        Document document = new()
        {
            Id = Id,
            UploadDate = DateTime.Now,
            FileContent = fileContent,
            FileName = file.FileName,
            FileSize = file.Length,
            FileType = file.ContentType
        };

        _updateDocumentRepository.Update(document);
    }

    public async Task DeleteAsync(int id)
    {
        bool exists = await _queryDocumentRepository.IsExisting(x => x.Id == id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        _deleteDocumentRepository.RemoveById(id);
    }

    public async Task<int> UploadLogoAsync(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var fileContent = memoryStream.ToArray();

        var fileMimeType = GetFileMimeType(fileContent);
        if (!FileConstraints.AllowedLogoFileMimeTypes.Contains(fileMimeType))
            throw new InvalidOperationException(_localizationService.GetLocalizedString("InvalidFileType"));

        if (fileContent.Length > FileConstraints.MaxLogoFileSize)
            throw new InvalidOperationException(_localizationService.GetLocalizedString("InvalidPermittedLimit"));

        return await AddAsync(file);
    }

    public async Task ChangeLogoAsync(IFormFile file, int Id)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var fileContent = memoryStream.ToArray();

        var fileMimeType = GetFileMimeType(fileContent);
        if (!FileConstraints.AllowedLogoFileMimeTypes.Contains(fileMimeType))
            throw new InvalidOperationException(_localizationService.GetLocalizedString("InvalidFileType"));

        if (fileContent.Length > FileConstraints.MaxLogoFileSize)
            throw new InvalidOperationException(_localizationService.GetLocalizedString("InvalidPermittedLimit"));

        await UpdateAsync(file, Id);
    }

    private string GetFileMimeType(byte[] fileContent)
    {
        if (fileContent == null || fileContent.Length < 4)
            return "unknown/unknown";

        // Dosya imzaları
        var jpgSignature = new byte[] { 0xFF, 0xD8, 0xFF };
        var pngSignature = new byte[] { 0x89, 0x50, 0x4E, 0x47 };
        var pdfSignature = new byte[] { 0x25, 0x50, 0x44, 0x46 };
        var docxSignature = new byte[] { 0x50, 0x4B, 0x03, 0x04 }; // DOCX, XLSX ve PPTX aynı imzayı paylaşır
        var xlsSignature = new byte[] { 0xD0, 0xCF, 0x11, 0xE0 }; // Eski XLS ve DOC dosyaları için (OLE formatı)

        // MIME türü kontrolü
        if (fileContent.Take(3).SequenceEqual(jpgSignature))
            return "image/jpeg";
        if (fileContent.Take(4).SequenceEqual(pngSignature))
            return "image/png";
        if (fileContent.Take(4).SequenceEqual(pdfSignature))
            return "application/pdf";
        if (fileContent.Take(4).SequenceEqual(docxSignature))
        {
            return "application/vnd.openxmlformats-officedocument.wordprocessingml.document"; // DOCX
        }
        if (fileContent.Take(4).SequenceEqual(xlsSignature))
        {
            // Eski XLS veya DOC
            return "application/vnd.ms-excel"; // XLS
        }

        return "unknown/unknown";
    }


    public async Task<int> UploadDocument(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var fileContent = memoryStream.ToArray();

        var fileMimeType = GetFileMimeType(fileContent);
        if (!FileConstraints.AllowedDocumentFileMimeTypes.Contains(fileMimeType))
            throw new InvalidOperationException(_localizationService.GetLocalizedString("InvalidFileType"));

        if (fileContent.Length > FileConstraints.MaxDocumentFileSize)
            throw new InvalidOperationException(_localizationService.GetLocalizedString("InvalidPermittedLimit"));

        return await AddAsync(file);
    }
}
