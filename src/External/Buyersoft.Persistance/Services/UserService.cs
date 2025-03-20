using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Pagination;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly ILocalizationService _localizationService;
    private readonly IDocumentService _documentService;
    private readonly IMapper _mapper;

    public UserService(UserManager<User> userManager, ILocalizationService localizationService, IMapper mapper, IDocumentService documentService)
    {
        _userManager = userManager;
        _localizationService = localizationService;
        _mapper = mapper;
        _documentService = documentService;
    }

    public async Task AddAsync(int companyId, UserCreateDto entity)
    {
        var exist = await _userManager.FindByEmailAsync(entity.Email);

        if (exist != null)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEmail"));
        }

        await _userManager.CreateAsync(new User
        {
            UserName = entity.Email,
            Email = entity.Email,
            Name = entity.Name,
            Surname = entity.Surname,
            DepartmentId = entity.DepartmentId,
            RoleId = entity.RoleId,
            Title = entity.Title,
            PhoneNumber = entity.PhoneNumber,
            CompanyId = companyId,
        }, entity.Password);
    }

    public async Task ChangePasswordAsync(int userId, UpdatePasswordDto model)
    {
        var entity = await _userManager.FindByIdAsync(userId.ToString());

        if (entity == null)
        {
            throw new InvalidOperationException("UserNotFound");
        }

        var changePassword = await _userManager.CheckPasswordAsync(entity, model.Password);

        if(!changePassword)
            throw new InvalidOperationException("Şifreniz yanlış!");

        await _userManager.ChangePasswordAsync(entity, model.Password, model.NewPassword); 
    }

    public async Task DeleteAsync(int id, int companyId)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user.CompanyId == companyId)
            await _userManager.DeleteAsync(user);
    }

    public async Task<IList<UserListDto>> GetAllAsync(int companyId, UserFilterDto filter)
    {
        var list = await _userManager.Users.Where(x => x.CompanyId == companyId).ToListAsync();

        return _mapper.Map<List<UserListDto>>(list);
    }

    public async Task<UserDetailDto> GetCurrentUser(int userId)
    {
        var user = await _userManager.Users.Where(x => x.Id == userId)
            .Include(x => x.UserPhoto)
            .Include(x => x.Role).ThenInclude(c => c.RolePermissions).ThenInclude(c=>c.Permission)
            .FirstOrDefaultAsync();

        return _mapper.Map<UserDetailDto>(user);
    }

    public async Task<IList<SelectListItemDto>> GetOwnerUsers(int companyId)
    {
        var list = await _userManager.Users.Where(x => x.CompanyId == companyId && x.Role.RolePermissions.Any(x => x.Permission.Name == "requests.owner"))
            .Select(x => new SelectListItemDto(x.Id, $"{x.Name} {x.Surname}")).ToListAsync();

        return list;
    }

    public async Task<PaginatedList<UserListDto>> GetPaginationListAsync(int companyId, UserFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();
        var query = _userManager.Users.Where(x => x.CompanyId == companyId);

        if (filter != null)
        {
            query = query.Where(x =>
                (string.IsNullOrEmpty(filter.Name) || x.Name.ToLower().Contains(filter.Name.ToLower())) &&
                (string.IsNullOrEmpty(filter.Surname) || x.Surname.ToLower().Contains(filter.Surname.ToLower())) &&
                (string.IsNullOrEmpty(filter.Email) || x.Email.ToLower().Contains(filter.Email.ToLower())) &&
                (string.IsNullOrEmpty(filter.Title) || x.Title.ToLower().Contains(filter.Title.ToLower())) &&
                (filter.RoleId == default || x.RoleId == filter.RoleId) &&
                (filter.DepartmentId == default || x.DepartmentId == filter.DepartmentId)
            );
        }

        query = query.AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<UserListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();


        return new PaginatedList<UserListDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<IList<SelectListItemDto>> GetSelectListItemAsync(int companyId)
    {
        var list = await _userManager.Users.Where(x => x.CompanyId == companyId)
            .Select(x => new SelectListItemDto(x.Id, $"{x.Name} {x.Surname}")).ToListAsync();

        return list;

    }

    public async Task UpdateAsync(int companyId, UserUpdateDto model)
    {
        var exist = await _userManager.FindByEmailAsync(model.Email);

        var user = await _userManager.FindByIdAsync(model.Id.ToString());

        if (exist != null && exist.Id != model.Id)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEmail"));
        }

        user.UserName = model.Email;
        user.Email = model.Email;
        user.Name = model.Name;
        user.Surname = model.Surname;
        user.DepartmentId = model.DepartmentId;
        user.RoleId = model.RoleId;
        user.Title = model.Title;
        user.PhoneNumber = model.PhoneNumber;
        user.CompanyId = companyId;

        await _userManager.UpdateAsync(user);
    }

    public async Task UpdateProfileAsync(int userId, UpdateProfileDto model)
    {

        var entity = await _userManager.FindByIdAsync(userId.ToString());

        if (entity == null)
        {
            throw new InvalidOperationException("UserNotFound");
        }

        if (model.UserPhoto != null)
        {
            if (entity.UserPhotoId == null)
            {
                int fileId = await _documentService.UploadLogoAsync(model.UserPhoto);
                entity.UserPhotoId = fileId;
            }
            else
            {
                await _documentService.ChangeLogoAsync(model.UserPhoto, entity.UserPhotoId.Value);
            }
        }

        entity.Name = model.Name;
        entity.Surname = model.Surname;
        entity.Title = model.Title;
        entity.PhoneNumber = model.PhoneNumber;
        entity.Email = model.Email;
        entity.ChoosenLanguage = model.ChoosenLanguage;

        await _userManager.UpdateAsync(entity);
    }
}
