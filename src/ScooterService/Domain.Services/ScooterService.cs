namespace ScooterRental.Domain.Services
{
    using global::Infrastructure.CrossCutting.Interfaces;
    using global::Infrastructure.CrossCutting.MessageBroker;

    using ScooterRental.Domain.Interfaces.Data.Repositories;
    using ScooterRental.Domain.Interfaces.Services;
    using ScooterRental.Domain.Models;

    using System;
    using System.Threading.Tasks;

    public class ScooterService : IScooterService
    {
        private readonly IScooterRepository scooterRepository;
        private readonly IQueue queue;

        public ScooterService(IScooterRepository scooterRepository, IQueue queue)
        {
            this.scooterRepository = scooterRepository;
            this.queue = queue;
        }

        public Scooter Rent(int scooterId, string passportNumber)
        {
            try
            {
                var scooter = this.scooterRepository.Get(scooterId);
                scooter.Rent(passportNumber);
                this.scooterRepository.Update(scooter);

                this.SendScooterLocation(scooterId);

                return scooter;
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }

        public Scooter TurnBack(int scooterId)
        {
            try
            {
                var scooter = this.scooterRepository.Get(scooterId);
                scooter.TurnBack();
                this.scooterRepository.Update(scooter);

                this.SendScooterLocation(scooterId);

                return scooter;
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }

        private void SendScooterLocation(int scooterId)
        {
            var trackingMessage = new TrackingMessage(
                scooterId, new Random().Next(1, 200));

            Task.Run(() => queue.SendMessageAsync(trackingMessage));
        }
    }
}
