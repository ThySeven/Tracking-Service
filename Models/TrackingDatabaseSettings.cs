namespace Tracking_Service.Models;

public class TrackingDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string DeliveryCollectionName { get; set; } = null!;
}