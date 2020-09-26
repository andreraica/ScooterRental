using ScooterRental.Domain.Interfaces.Services;
using ScooterRental.Presentation.ScooterRental.WebAPI.DTO;

namespace ScooterRental.Presentation.ScooterRental.WebAPI.Conrollers
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class TrackingController : Controller
    {
        private readonly ITrackingService trackingService;

        public TrackingController(ITrackingService trackingService)
        {
            this.trackingService = trackingService;
        }
        
        [HttpGet("/GetByScooter/{scooterId}")]
        public IActionResult GetByScooter(int scooterId)
        {
            var tracking = trackingService.GetByScooterId(scooterId);

            if (tracking == null)
                return new NoContentResult();

            return Ok(new TrackingDto(Convert.ToInt32(tracking.Id), tracking.ScooterId, tracking.LocationId));
        }

        [HttpPost]
        public void Insert(int scooterId, int locationId)
        {
            //ADD QUEUE
            trackingService.Insert(scooterId, locationId);
        }
    }
}
