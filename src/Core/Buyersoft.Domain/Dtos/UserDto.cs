using Microsoft.AspNetCore.Http;

namespace Buyersoft.Domain.Dtos;

public sealed record UserDto(
    int Id,
    string Name,
    string Surname,
    string Email,
    string PhoneNumber,
    int RoleId,
    int CompanyId,
    int DepartmentId,
    string Title,
    string ImageUrl,
    string ChoosenLanguage,
    string Code,
    int? DocumentId);

public sealed record UserCreateDto(
    string Name,
    string Surname,
    string Title,
    string Email,
    string PhoneNumber,
    int RoleId,
    int CompanyId,
    int DepartmentId,
    string Password);

public sealed record UserUpdateDto(
    int Id,
    string Name,
    string Surname,
    string Title,
    string Email,
    string PhoneNumber,
    int RoleId,
    int CompanyId,
    int DepartmentId,
    string Password);

public sealed record UserFilterDto(string Name,
    string Surname,
    string Email,
    int RoleId,
    int CompanyId,
    int DepartmentId,
    string Title,
    string ImageUrl,
    string ChoosenLanguage,
    string Code);

public class UserListDto()
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Title { get; set; }
    public string Email { get; set; }
    public string RoleName { get; set; }
    public string CompanyName { get; set; }
    public string DepartmentName { get; set; }
    public string PhoneNumber { get; set; }
    public int RoleId { get; set; }
    public int CompanyId { get; set; }
    public int DepartmentId { get; set; }
    public string UserPhotoUrl { get; set; }

}

public class UserDetailDto()
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Title { get; set; }
    public string Email { get; set; }
    public string RoleName { get; set; }
    public string CompanyName { get; set; }
    public string DepartmentName { get; set; }
    public string PhoneNumber { get; set; }
    public int RoleId { get; set; }
    public int CompanyId { get; set; }
    public int DepartmentId { get; set; }
    public string UserPhotoUrl { get; set; }
    public string ChoosenLanguage { get; set; }
    public string[] OperationOfRole { get; set; }
}

public sealed record UpdatePasswordDto(string Password, string NewPassword);

public sealed record UpdateProfileDto(string Name, string Surname, string PhoneNumber, string Title, string ChoosenLanguage, string Email, IFormFile UserPhoto);