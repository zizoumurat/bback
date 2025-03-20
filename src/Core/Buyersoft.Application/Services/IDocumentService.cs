using Buyersoft.Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace Buyersoft.Application.Services;
public interface IDocumentService
{
    Task<int> AddAsync(IFormFile file);

    Task<int> UploadLogoAsync(IFormFile file);  
    Task<int> UploadDocument(IFormFile file);
    Task ChangeLogoAsync(IFormFile file, int Id);
    Task UpdateAsync(IFormFile file, int Id);

    Task DeleteAsync(int id);
}
