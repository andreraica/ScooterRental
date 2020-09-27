namespace Infrastructure.CrossCutting.MessageBroker
{
    using Infrastructure.CrossCutting.Interfaces;

    using Microsoft.Azure.ServiceBus;

    using Newtonsoft.Json;

    using System;
    using System.Text;
    using System.Threading.Tasks;

    public class Queue : IQueue
    {
        private readonly IQueueClient queueClient;

        public Queue()
        {
            this.queueClient = new QueueClient(
                Environment.GetEnvironmentVariable("TRACKING_QUEUE_CONNECTION"),
                Environment.GetEnvironmentVariable("TRACKING_QUEUE_NAME"));
        }

        public async Task SendMessageAsync(TrackingMessage trackingMessage)
        {
            try
            {
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
