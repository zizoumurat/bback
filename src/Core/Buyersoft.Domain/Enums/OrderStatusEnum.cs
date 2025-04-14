namespace Buyersoft.Domain.Enums;

public enum OrderStatusEnum
{
    OrderPending,        // Sipariş Beklemede
    OrderCancelled,      // Sipariş İptal Edildi
    InProduction,        // Sipariş Üretim Aşamasında
    InShipment,          // Sipariş Sevk Aşamasında
    Shipped,             // Sipariş Sevk Edildi
    Delivered,           // Teslim Alındı
    NonconformityReported, // Uygunsuzluk Bildirildi
    OrderCompleted
}
