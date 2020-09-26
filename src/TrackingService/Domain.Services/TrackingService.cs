namespace ScooterRental.Domain.Services
{
    using System;
    using ScooterRental.Domain.Interfaces.Data.Repositories;
    using ScooterRental.Domain.Interfaces.Services;
    using ScooterRental.Domain.Models;

    public class TrackingService : ITrackingService
    {
        private readonly ITrackingRepository trackingRepository;

        public TrackingService(ITrackingRepository trackingRepository)
        {
            this.trackingRepository = trackingRepository;
        }

        public Tracking GetByScooterId(int scooterId)
        {
            try
            {
                var tracking = this.trackingRepository.GetByScooter(scooterId);
                return tracking;
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }

        public void Insert(int scooterId, int locationId)
        {
            try
            {
                this.trackingRepository.Insert(new Tracking(scooterId, locationId));
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }
    }
}
