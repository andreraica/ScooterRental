namespace ScooterRental.Domain.Models
{
    public class Tracking
    {
        public Tracking(int scooterId, int locationId)
        {
            this.ScooterId = scooterId;
            this.LocationId = locationId;
        }
        public Tracking(long id, int scooterId, int locationId)
        {
            this.Id = id;
            this.ScooterId = scooterId;
            this.LocationId = locationId;
        }

        public long Id { get; private set; }

        public int ScooterId { get; private set; }

        public int LocationId { get; private set; }
    }
}
