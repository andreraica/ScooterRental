namespace ScooterRental.Domain.Services
{
    using global::Infrastructure.CrossCutting.Interfaces;
    using global::Infrastructure.CrossCutting.MessageBroker;

    using ScooterRental.Domain.Interfaces.Data.Repositories;
    using ScooterRental.Domain.Interfaces.Services;
    using ScooterRental.Domain.Models;

    using System;
    using System.Threading.Tasks;

    public class TrackingService : ITrackingService
    {
        private readonly ITrackingRepository trackingRepository;
        private readonly IQueue queue;

        public TrackingService(ITrackingRepository trackingRepository, IQueue queue)
        {
            this.trackingRepository = trackingRepository;
            this.queue = queue;
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
                var trackingMessage = new TrackingMessage(
                scooterId, locationId);

                Task.Run(() => queue.SendMessageAsync(trackingMessage));
                //this.trackingRepository.Insert(new Tracking(scooterId, locationId));
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }
    }
}
