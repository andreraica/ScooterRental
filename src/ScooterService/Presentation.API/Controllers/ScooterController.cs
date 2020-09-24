using ScooterRental.Domain.Interfaces.Services;
using ScooterRental.Presentation.ScooterRental.WebAPI.DTO;

namespace ScooterRental.Presentation.ScooterRental.WebAPI.Conrollers
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ScooterController : Controller
    {
        private readonly IScooterService scooterService;

        public ScooterController(IScooterService scooterService)
        {
            this.scooterService = scooterService;
        }
        
        [HttpPost("/Rent/{scooterId}/{passportNumber}")]
        public ScooterResponse Rent(int scooterId, string passportNumber)
        {
            var scooter = scooterService.Rent(scooterId, passportNumber);

            var scooterResponse = new ScooterResponse(Convert.ToInt32(scooter.Id));
            
            return scooterResponse;
        }

        [HttpPost("/TurnBack/{scooterId}")]
        public ScooterResponse TurnBack(int scooterId)
        {
            var scooter = scooterService.TurnBack(scooterId);

            var scooterResponse = new ScooterResponse(Convert.ToInt32(scooter.Id));

            return scooterResponse;
        }
    }
}
