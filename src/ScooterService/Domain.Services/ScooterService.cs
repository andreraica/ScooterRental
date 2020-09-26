namespace ScooterRental.Domain.Services
{
    using Microsoft.Azure.ServiceBus;

    using Newtonsoft.Json;

    using ScooterRental.Domain.Interfaces.Data.Repositories;
    using ScooterRental.Domain.Interfaces.IoC;
    using ScooterRental.Domain.Interfaces.Services;
    using ScooterRental.Domain.Models;

    using System;
    using System.Text;
    using System.Threading.Tasks;

    public class ScooterService : IScooterService
    {
        const string ServiceBusConnectionString = "Endpoint=sb://scotterrentalqueue.servicebus.windows.net/;SharedAccessKeyName=SystemUser;SharedAccessKey=lmQRuxHRnlkm5iBJBJN8NJnmewtTAMqFrLo1JiYPoFw=;";
        const string QueueName = "tracking";
        static IQueueClient queueClient;

        private readonly IScooterRepository scooterRepository;
        private readonly ISettings settings;

        public ScooterService(IScooterRepository scooterRepository, ISettings settings)
        {
            this.scooterRepository = scooterRepository;
            this.settings = settings;
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
            var trackingMessage = new DTO.TrackingMessage(
                scooterId, new Random().Next(1, 200));

            Task.Run(() => this.SendMessageAsync(trackingMessage));
        }

        private async Task SendMessageAsync(DTO.TrackingMessage trackingMessage)
        {
            try
            {
                queueClient = new QueueClient(
                    this.settings.GetAppSetting("TrackingQueueConnectionString"),
                    this.settings.GetAppSetting("TrackingQueueName"));

                var message = new Message(
                    Encoding.UTF8.GetBytes(
                        JsonConvert.SerializeObject(trackingMessage).ToString()
                        )
                    );

                await queueClient.SendAsync(message).ConfigureAwait(false);

                await queueClient.CloseAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
