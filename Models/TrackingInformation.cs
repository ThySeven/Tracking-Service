
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Tracking_Service.Models
{
    public class TrackingInformation
    {
        public int TrackingId { get; set; }
        public required string Carrier { get; set; }

        public List<StatusList> StatusUpdates { get; set; }







    }
}

