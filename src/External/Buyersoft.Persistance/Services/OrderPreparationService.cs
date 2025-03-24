using AutoMapper;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Enums;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.OfferRepositories;
using Buyersoft.Domain.Repositories.OrderPreparationRepositories;
using Buyersoft.Domain.Repositories.RequestRepositories;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using MimeKit;
using Buyersoft.Domain.Repositories.CompanyRepositories;
using System.Linq;

namespace Buyersoft.Persistance.Services;
public class OrderPreparationService : IOrderPreparationService
{
    private readonly IAddOrderPreparationRepository _addOrderPreparationRepository;
    private readonly IUpdateOrderPreparationRepository _updateOrderPreparationRepository;
    private readonly IDeleteOrderPreparationRepository _deleteOrderPreparationRepository;
    private readonly IQueryOrderPreparationRepository _queryOrderPreparationRepository;
    private readonly IQueryRequestRepository _queryRequestRepository;
    private readonly IQueryOfferRepository _queryOfferRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IDocumentService _documentService;
    private readonly ICompanyService _companyService;
    private readonly IMapper _mapper;

    public OrderPreparationService(IAddOrderPreparationRepository addOrderPreparationRepository,
        IUpdateOrderPreparationRepository updateOrderPreparationRepository,
        IDeleteOrderPreparationRepository deleteOrderPreparationRepository,
        IQueryOrderPreparationRepository queryOrderPreparationRepository,
        ILocalizationService localizationService,
        IMapper mapper,
        IQueryRequestRepository queryRequestRepository,
        IQueryOfferRepository queryOfferRepository,
        IDocumentService documentService,
        ICompanyService companyService)
    {
        _addOrderPreparationRepository = addOrderPreparationRepository;
        _updateOrderPreparationRepository = updateOrderPreparationRepository;
        _deleteOrderPreparationRepository = deleteOrderPreparationRepository;
        _queryOrderPreparationRepository = queryOrderPreparationRepository;
        _localizationService = localizationService;
        _mapper = mapper;
        _queryRequestRepository = queryRequestRepository;
        _queryOfferRepository = queryOfferRepository;
        _documentService = documentService;
        _companyService = companyService;
    }

    public async Task AddAsync(int companyId, int RequestId, int OfferId)
    {
        var offer = await _queryOfferRepository.GetFirstAsync(x => x.Id == OfferId)
            .Include(x => x.Request)
                .ThenInclude(x => x.Template)
            .Include(x => x.Request)
                .ThenInclude(x => x.Category)
                    .ThenInclude(x => x.MainCategory)
            .Include(x => x.Request)
                .ThenInclude(x => x.Category)
                    .ThenInclude(x => x.SubCategory)
            .Include(x => x.Request)
                .ThenInclude(x => x.Category)
                    .ThenInclude(x => x.RequestGroup)

            .FirstAsync();

        OrderPreparation addEntity = new()
        {
            CompanyId = companyId,
            RequestId = RequestId,
            OfferId = OfferId,
            MainCategory = offer.Request.Category.MainCategory.Name,
            SubCategory = offer.Request.Category.SubCategory.Name,
            RequestGroup = offer.Request.Category.RequestGroup.Name,
            RequestCode = offer.Request.RequestCode,
            ReferenceCode = offer.ReferenceCode,
            AvailableLimit = true
        };

        await _addOrderPreparationRepository.AddAsync(addEntity);
    }

    public async Task CreateOrder(OrderCreateDto Model)
    {
        var orderPreparation = await _queryOrderPreparationRepository.GetFirstAsync(x => x.Id == Model.OrderPreparationId)
             .Include(x => x.Orders)
                 .ThenInclude(x => x.OrderItems)
             .Include(x => x.Offer)
                .ThenInclude(x => x.OfferDetails)
             .FirstOrDefaultAsync();

        if (orderPreparation == null)
        {
            throw new Exception("OrderPreparation not found");
        }

        if (Model.OrderItems.Sum(x => x.Quantity) == 0)
        {
            throw new Exception("EmptyOrder");
        }

        var supplier = await _companyService.GetCurrentCompany(orderPreparation.Offer.CompanyId);
        var company = await _companyService.GetCurrentCompany(1);

        var orderCode = $"{DateTime.Now:MMdd}{new Random().Next(1000, 9999)}";

        var orderFileContent = GeneratePurchaseOrderPdf(supplier, company, orderCode, Model.OrderItems.Sum(x => x.Quantity * x.UnitPrice), Model.OrderItems);


        var orderFileId = await _documentService.AddAsync(orderFileContent, $"PurchaseOrder_{orderCode}.pdf", "application/pdf");


        var order = new Order()
        {
            OrderPreparationId = Model.OrderPreparationId,
            OrderCode = orderCode,
            Status = OrderStatusEnum.OrderPending,
            TotalPrice = Model.OrderItems.Sum(x => x.Quantity * x.UnitPrice),
            OrderDate = DateTime.Now,
            DocumentId = orderFileId,
            OrderItems = Model.OrderItems.Where(x => x.Quantity > 0).Select(x => new OrderItem()
            {
                OfferDetailId = x.OfferDetailId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                TotalPrice = x.Quantity * x.UnitPrice,
                ProductDefinition = x.ProductDefinition
            }).ToList()
        };
        orderPreparation.Orders.Add(order);

        foreach (var item in orderPreparation.Orders)
        {
            foreach (var offerDetailGroup in item.OrderItems.GroupBy(x => x.OfferDetailId))
            {
                int totalQuantityForOrderItems = offerDetailGroup.Sum(x => x.Quantity);
                int quantityForOfferDetails = orderPreparation.Offer.OfferDetails.First(x => x.Id == offerDetailGroup.Key).Quantity;

                if (totalQuantityForOrderItems > quantityForOfferDetails)
                {
                    throw new Exception("Sipariş miktari olması gerekenden fazla olduğu için işlem gerçekleştirilemedi.");
                }
            }
        }

        var totalOrderPrice = orderPreparation.Orders.Sum(x => x.TotalPrice);
        orderPreparation.TotalPrice = totalOrderPrice;
        orderPreparation.AvailableLimit = true;
        _updateOrderPreparationRepository.Update(orderPreparation);
    }

    public async Task<PaginatedList<OrderPreparationListDto>> GetAllAsync(int companyId, OrderPreparationFilterDto filter, PageRequest pagination)
    {
        var query = _queryOrderPreparationRepository.GetList(x => x.CompanyId == companyId)
            .Include(x => x.Request)
                .ThenInclude(x => x.Category)
            .Include(x => x.Request)
                .ThenInclude(x => x.Currency)
            .Include(x => x.Orders)
                .ThenInclude(x => x.OrderItems)
                    .ThenInclude(x => x.OfferDetail)
            .Include(x => x.Offer)
                .ThenInclude(x => x.OfferDetails)
            .Include(x => x.Offer)
                .ThenInclude(x => x.Company)
            .Select(x => new OrderPreparationListDto()
            {
                Id = x.Id,
                RequestId = x.RequestId,
                OfferId = x.OfferId,
                Supplier = x.Offer.Company.Name,
                MainCategory = x.MainCategory,
                SubCategory = x.SubCategory,
                RequestGroup = x.RequestGroup,
                RequestCode = x.RequestCode,
                CurrencyCode = x.Request.Currency.Code,
                ReferenceCode = x.ReferenceCode,
                Unit = x.Request.Category.Unit,
                OrderCount = x.Orders.Count,
                TotalPrice = x.TotalPrice,
                AvailableLimit = x.AvailableLimit,
                OfferDetailList = x.Offer.OfferDetails.Select(od => new OfferDetailListDto(
                    od.Id,
                    od.ProductDefinition,
                    od.UnitPrice,
                    od.Quantity - x.Orders
                        .SelectMany(o => o.OrderItems) // Tüm OrderItem'ları düzleştir
                        .Where(oi => oi.OfferDetailId == od.Id) // Sadece ilgili OfferDetailId'leri filtrele
                        .Sum(oi => oi.Quantity) // Toplam Quantity'yi hesapla
                )).ToList(),
                Orders = x.Orders.Select(or => new OrderListDto(
                    or.Id,
                    or.OrderCode,
                    or.TotalPrice,
                    or.Status,
                    or.OrderDate,
                    or.OrderItems.Select(oi => new OrderItemListDto(
                        oi.Id,
                        oi.OfferDetailId,
                        oi.ProductDefinition,
                        oi.UnitPrice,
                        oi.TotalPrice,
                        oi.Quantity
                    )).ToList(),
                    or.Document != null ? Convert.ToBase64String(or.Document.FileContent) : "",
                    or.Document != null ? or.Document.FileName : ""
                )).ToList()
            })
            .AsQueryable();


        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
        .ToListAsync();


        return new PaginatedList<OrderPreparationListDto>(items, count, pagination.Page, pagination.PageSize);

    }

    private byte[] GeneratePurchaseOrderPdf(CompanyDetailDto supplier, CompanyDetailDto company, string PO, decimal totalPrice, List<OrderItemCreateDto> OrderItems)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        byte[] logoContent = supplier.LogoContent;

        var document = QuestPDF.Fluent.Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);

                page.Header().Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Satın Alma Siparişi").FontSize(20).Bold();

                    row.ConstantItem(80).AlignRight().Height(50).Image(company.LogoContent).FitArea(); // Resmi ekleme
                });

                page.Content().Column(col =>
                {
                    col.Item().Row(row =>
                    {
                        row.RelativeItem()
                            .AlignRight()
                            .Column(column =>
                            {
                                column.Item().Text(company.Name).FontSize(18).Bold();
                            });
                    });



                    col.Item().LineHorizontal(0.5f);

                    col.Item().Row(row =>
                    {
                        row.RelativeItem(1).Border(1).Padding(10).Column(column =>
                        {
                            column.Item().Text("Tedarikçi Firma Fatura Bilgileri").Bold();
                        });

                        row.RelativeItem(1).Border(1).Padding(10).Column(column =>
                        {
                            column.Item().Text($"{PO} / {DateTime.Now.Date.ToString("dd/MM/yyyy")}");
                            column.Item().Text(company.ContactFirstName + " " + company.ContactLastName);
                            column.Item().Text(company.Email);
                        });
                    });

                    col.Item().Row(row =>
                    {
                        row.RelativeItem(1).Border(1).Padding(10).Column(column =>
                        {
                            column.Item().Text(company.Address).Bold();
                        });
                    });

                    col.Item().LineHorizontal(0.5f);

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(50);  // Sipariş No
                            columns.RelativeColumn();    // Ürün / Hizmet Tanımı
                            columns.ConstantColumn(100); // Birim Fiyat / Miktar
                            columns.ConstantColumn(120); // Net Birim Fiyat
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("#").Bold();
                            header.Cell().Element(CellStyle).Text("Ürün / Hizmet Tanımı").Bold();
                            header.Cell().Element(CellStyle).Text("Birim Fiyat / Miktar").Bold();
                            header.Cell().Element(CellStyle).Text("Net Birim Fiyat").Bold();
                        });

                        int orderNumber = 1;
                        foreach (var item in OrderItems)
                        {
                            table.Cell().Element(CellStyle).Text(orderNumber.ToString());
                            table.Cell().Element(CellStyle).Text(item.ProductDefinition);
                            table.Cell().Element(CellStyle).Text($"{item.UnitPrice} TRY / {item.Quantity}");
                            table.Cell().Element(CellStyle).Text($"{item.UnitPrice * item.Quantity} TRY");
                            orderNumber++;
                        }

                        static IContainer CellStyle(IContainer container) =>
                            container.BorderBottom(1).PaddingVertical(5).AlignCenter();
                    });

                    col.Item().LineHorizontal(0.5f);

                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Text("Toplam Net Tutar");
                        row.RelativeItem().AlignRight().Text($"{totalPrice} (TRY)");
                    });

                    col.Item().LineHorizontal(0.5f);

                    col.Item().Text("TR" + company.BankInfos.First().IBAN ?? "").Bold();
                });

                page.Footer()
                    .AlignCenter()
                    .Text("Buyersoft.");
            });
        });

        using var memoryStream = new MemoryStream();
        document.GeneratePdf(memoryStream);
        return memoryStream.ToArray();
    }

}