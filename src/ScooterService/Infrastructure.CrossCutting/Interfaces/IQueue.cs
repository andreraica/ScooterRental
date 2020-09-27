namespace Infrastructure.CrossCutting.Interfaces
{
    using Infrastructure.CrossCutting.MessageBroker;

    using System.Threading.Tasks;

    public interface IQueue
    {
        Task SendMessageAsync(TrackingMessage trackingMessage);
    }
}