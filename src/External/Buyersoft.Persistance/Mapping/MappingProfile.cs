using AutoMapper;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Entitites.Identity;

namespace Buyersoft.Persistance.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region ApprovalChain
        CreateMap<ApprovalChainDto, ApprovalChain>().ReverseMap();
        CreateMap<ApprovalChain, ApprovalChainListDto>()
            .ForMember(dest => dest.CurrencyName, opt => opt.MapFrom(src => src.Currency.Name))
            .ForMember(dest => dest.UserIdList, opt => opt.MapFrom(src => src.ApprovalChainUsers.Select(acu => acu.UserId).ToList()))
            .ForMember(dest => dest.OwnerUserList, opt => opt.MapFrom(src => src.ApprovalChainUsers.Select(acu => $"{acu.User.Name} {acu.User.Surname}").ToList()));
        #endregion

        #region BankInfo
        CreateMap<BankInfoDto, BankInfo>().ReverseMap();
        CreateMap<BankInfo, BankInfoListDto>()
            .ForMember(dest => dest.CurrencyName, opt => opt.MapFrom(src => src.Currency.Name))
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.Branch.CityId))
            .ForMember(dest => dest.DistrictId, opt => opt.MapFrom(src => src.Branch.DistrictId))
            .ForMember(dest => dest.BankName, opt => opt.MapFrom(src => src.Branch.BankName))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.BranchName));
        #endregion

        CreateMap<BankInfoDto, BankInfo>().ReverseMap();

        #region Branch
        CreateMap<BranchDto, Branch>().ReverseMap();
        CreateMap<Branch, BranchListDto>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
            .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.District.Name));
        #endregion

        #region Supplier
        CreateMap<SupplierDto, Supplier>().ReverseMap();
        CreateMap<Supplier, SupplierListDto>();
        #endregion

        #region Budget
        CreateMap<BudgetDto, Budget>().ReverseMap();
        CreateMap<Budget, BudgetListDto>()
            .ForMember(dest => dest.CurrencyName, opt => opt.MapFrom(src => src.Currency.Name))
            .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.Currency.Code))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
        #endregion

        #region Category
        CreateMap<CategoryDto, Category>().ReverseMap();
        CreateMap<Category, CategoryListDto>()
            .ForMember(dest => dest.OwnerUserList, opt => opt.MapFrom(src => src.CategoryUsers.Select(x => x.User.Name + " " + x.User.Surname).ToList()))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => $"{src.MainCategory.Name} > {src.SubCategory.Name} > {src.RequestGroup.Name}"))
            .ForMember(dest => dest.CompanySubCategoryName, opt => opt.MapFrom(src => src.SubCategory.CompanySubCategory.Name))
            .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategory.CompanySubCategory.Id))
            .ForMember(dest => dest.RequestGroupId, opt => opt.MapFrom(src => src.RequestGroup.CompanyRequestGroup.Id))
            .ForMember(dest => dest.CompanyRequestGroupName, opt => opt.MapFrom(src => src.RequestGroup.CompanyRequestGroup.Name))
            .ForMember(dest => dest.UserIdList, opt => opt.MapFrom(src => src.CategoryUsers.Select(cs => cs.UserId).ToList()));
        CreateMap<Category, CategoryExcellListDto>()
            .ForMember(dest => dest.OwnerUserList, opt => opt.MapFrom(src => src.CategoryUsers.Select(x => x.User.Name + " " + x.User.Surname).ToList()))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => $"{src.MainCategory.Name} > {src.SubCategory.Name} > {src.RequestGroup.Name}"))
            .ForMember(dest => dest.CompanySubCategoryName, opt => opt.MapFrom(src => src.SubCategory.CompanySubCategory.Name))
            .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategory.CompanySubCategory.Id))
            .ForMember(dest => dest.RequestGroupId, opt => opt.MapFrom(src => src.RequestGroup.CompanyRequestGroup.Id))
            .ForMember(dest => dest.CompanyRequestGroupName, opt => opt.MapFrom(src => src.RequestGroup.CompanyRequestGroup.Name))
            .ForMember(dest => dest.UserIdList, opt => opt.MapFrom(src => src.CategoryUsers.Select(cs => cs.UserId).ToList()));
        #endregion

        #region City
        CreateMap<CityDto, City>().ReverseMap();
        #endregion

        CreateMap<DocumentDto, Document>().ReverseMap();

        #region Contract
        CreateMap<Contract, ContractListDto>()
            .ForMember(dest => dest.DocumentUrl, opt => opt.MapFrom(src => src.DocumentId != null ? Convert.ToBase64String(src.Document.FileContent) : ""))
            .ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src => src.DocumentId != null ? src.Document.FileName : ""))
            .ForMember(dest => dest.MimeType, opt => opt.MapFrom(src => src.DocumentId != null ? src.Document.FileType : ""))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Request.Manager.Name + " " + src.Request.Manager.Surname))
            .ForMember(dest => dest.Requester, opt => opt.MapFrom(src => src.Request.CreatedBy.Name + " " + src.Request.CreatedBy.Surname))
            .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Request.Offers.First(x => x.CompanyId == src.CompanyId).Company.Name));

        CreateMap<ContractComment, CommentListDto>()
           .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.Name));
        #endregion

        #region Company
        CreateMap<SupplierCreateDto, Company>().ReverseMap();
        CreateMap<CompanyDto, Company>().ReverseMap();
        CreateMap<Company, CompanyListDto>();
        CreateMap<UpdateCompanyDto, Company>()
            .ForMember(dest => dest.Logo, opt => opt.Ignore());
        CreateMap<Company, CompanyDetailDto>()
            .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => Convert.ToBase64String(src.Logo.FileContent)));
        #endregion

        #region Currency
        CreateMap<CurrencyDto, Currency>().ReverseMap();
        #endregion

        #region CurrencyParameter
        CreateMap<CurrencyParameterDto, CurrencyParameter>().ReverseMap();
        CreateMap<CurrencyParameter, CurrencyParameterListDto>();
        #endregion

        #region Department
        CreateMap<DepartmentDto, Department>().ReverseMap();
        CreateMap<Department, DepartmentListDto>();
        #endregion

        #region District
        CreateMap<DistrictDto, District>().ReverseMap();
        #endregion

        #region Location
        CreateMap<LocationDto, Location>().ReverseMap();
        CreateMap<Location, LocationListDto>();
        #endregion

        #region MainCategory
        CreateMap<MainCategoryDto, MainCategory>().ReverseMap();
        CreateMap<MainCategory, MainCategoryListDto>();
        #endregion

        #region Notification
        CreateMap<Notification, NotificationDto>().ReverseMap();
        #endregion

        #region OfferLimit
        CreateMap<OfferLimitDto, OfferLimit>().ReverseMap();
        CreateMap<OfferLimit, OfferLimitListDto>();
        #endregion

        #region Offer
        CreateMap<Offer, OfferListDto>()
            .ForMember(dest => dest.DocumentUrl, opt => opt.MapFrom(src => src.Document != null ? Convert.ToBase64String(src.Document.FileContent) : ""));
        CreateMap<OfferDetail, OfferDetailDto>().ReverseMap();
        #endregion

        #region ProductDefinition
        CreateMap<ProductDefinitionDto, ProductDefinition>().ReverseMap();
        #endregion

        #region ServiceDefinition
        CreateMap<ServiceDefinitionDto, ServiceDefinition>().ReverseMap();
        #endregion

        #region Request
        CreateMap<RequestDto, Request>().ReverseMap();
        CreateMap<CreateRequestDto, Request>().ReverseMap();
        CreateMap<Request, RequestListDto>()
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(s => s.CreatedBy.Name + " " + s.CreatedBy.Surname))
            .ForMember(dest => dest.Manager, opt => opt.MapFrom(s => s.Manager.Name + " " + s.Manager.Surname))
            .ForMember(dest => dest.LocationId, opt => opt.MapFrom(s => s.Category.LocationId))
            .ForMember(dest => dest.BudgetName, opt => opt.MapFrom(s => s.Budget != null ? s.Budget.BudgetTitle : ""))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(s => s.Budget != null && s.Budget.Currency != null ? s.Budget.Currency.Code : ""))
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(s => s.Category.Location.Name))
            .ForMember(dest => dest.MainCategoryId, opt => opt.MapFrom(s => s.Category.MainCategoryId))
            .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(s => s.Category.SubCategory.CompanySubCategory.Id))
            .ForMember(dest => dest.RequestGroupId, opt => opt.MapFrom(s => s.Category.RequestGroup.CompanyRequestGroup.Id));

        #endregion

        #region RequestGroup
        CreateMap<RequestGroupDto, RequestGroup>().ReverseMap();
        CreateMap<RequestGroup, RequestGroupListDto>();
        #endregion

        #region CompanyRequestGroup
        CreateMap<CompanyRequestGroupDto, CompanyRequestGroup>().ReverseMap();
        CreateMap<CompanyRequestGroup, CompanyRequestGroupListDto>();
        #endregion

        #region ReverseAuction
        CreateMap<ReverseAuction, ReverseAuctionListDto>()
            .ForMember(dest => dest.Statu, opt => opt.MapFrom(r => r.Statu))
            .ForMember(dest => dest.RequestCode, opt => opt.MapFrom(r => r.Request.RequestCode))
            .ForMember(dest => dest.RequestTitle, opt => opt.MapFrom(r => r.Request.Title))
            .ForMember(dest => dest.Times, opt => opt.MapFrom(r => new[] { r.StartTime, r.EndTime }))
            .ForMember(dest => dest.ParticipantList, opt => opt.MapFrom(r =>
                r.Request.Offers
                    .GroupBy(offer => offer.CompanyId)
                    .Select(group => new Participant
                    {
                        Name = group.FirstOrDefault().Company.Name,
                        Status = group.FirstOrDefault().AuctionParticipationStatus
                    }).ToList()
            ));

        CreateMap<AddReverseAuctionDto, ReverseAuction>();
        CreateMap<ReverseAuction, ReverseAuctionDto>().ReverseMap();
        #endregion

        #region Role
        CreateMap<RoleDto, Role>().ReverseMap();
        CreateMap<Role, RoleListDto>();
        #endregion

        #region SubCategory
        CreateMap<SubCategoryDto, SubCategory>().ReverseMap();
        CreateMap<SubCategory, SubCategoryListDto>();
        #endregion

        #region SupplierAction
        CreateMap<SupplierAction, SupplierActionListDto>();
        CreateMap<SupplierActionCreateDto, SupplierAction>();
        #endregion

        #region CompanySubCategory
        CreateMap<CompanySubCategoryDto, CompanySubCategory>().ReverseMap();
        CreateMap<CompanySubCategory, CompanySubCategoryListDto>();
        #endregion

        #region SystemNotification
        CreateMap<SystemNotificationDto, SystemNotification>().ReverseMap();
        CreateMap<SystemNotification, SystemNotificationListDto>();
        #endregion

        #region TaxOffice
        CreateMap<TaxOfficeDto, TaxOffice>().ReverseMap();
        #endregion

        #region Template
        CreateMap<TemplateDto, Template>().ReverseMap();
        CreateMap<Template, TemplateListDto>();
        #endregion

        #region User
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<User, UserListDto>();
        CreateMap<User, UserDetailDto>()
            .ForMember(dest => dest.OperationOfRole, opt => opt.MapFrom(src => src.Role.RolePermissions.Select(x => x.Permission.Name).ToArray()))
            .ForMember(dest => dest.UserPhotoUrl, opt => opt.MapFrom(src => Convert.ToBase64String(src.UserPhoto.FileContent)));
        #endregion
    }
}
