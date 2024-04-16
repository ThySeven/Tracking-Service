using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Tracking_Service.Models;
using Tracking_Service.Services;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly IMongoCollection<Delivery> _deliveries;
        private readonly ITracking _trackingService;

        public ParcelController(IMongoClient client, ITracking trackingService)
        {
            var database = client.GetDatabase("your_database_name");
            _deliveries = database.GetCollection<Delivery>("your_collection_name");
            _trackingService = trackingService;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParcel(string id, [FromBody] ParcelUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var filter = Builders<Delivery>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<Delivery>.Update
                    .Set("Status", request.Status)
                    .PushEach("TrackingInformation.StatusUpdates", request.TrackingInformation.StatusUpdates);

                var result = await _deliveries.UpdateOneAsync(filter, update);

                if (result.ModifiedCount == 1)
                {
                    // Call the tracking service to notify about the update
                    _trackingService.UpdateParcel(Guid.Parse(id));

                    return Ok(new { message = "Document updated successfully" });
                }
                else
                {
                    return NotFound(new { error = "Document not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }

    public class ParcelUpdateRequest
    {
        public string Status { get; set; }
        public TrackingInformation TrackingInformation { get; set; }
    }
}
