namespace Infrastructure.CrossCutting.MessageBroker
{
    public class TrackingMessage
    {
        public TrackingMessage(int scooterId, int locationId)
        {
            this.ScooterId = scooterId;
            this.LocationId = locationId;
        }

        public int ScooterId { get; private set; }
        public int LocationId { get; private set; }
    }
}
