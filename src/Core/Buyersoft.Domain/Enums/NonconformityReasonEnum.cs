namespace Buyersoft.Domain.Enums;

public enum NonconformityReasonEnum
{
    ProductQualityIssue,       // Ürün Kalite Problemi
    PackagingQualityIssue,     // Paketleme Kalite Problemi
    ShipmentQualityIssue,      // Sevkiyat Kalite Problemi
    MissingProductShipment,    // Eksik Ürün Sevkiyatı
    ExcessProductShipment,     // Fazla Ürün Sevkiyatı
    OccupationalSafetyIssue,   // İş Güvenliği Uygunsuzluğu
    Other                      // Diğer
}
