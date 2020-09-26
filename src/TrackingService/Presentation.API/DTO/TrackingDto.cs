namespace ScooterRental.Presentation.ScooterRental.WebAPI.DTO
{
    public class TrackingDto
    {
        public TrackingDto(int scooterId, int locationId)
        {
            this.ScooterId = scooterId;
            this.LocationId = locationId;
        }

        public TrackingDto(long id, int scooterId, int locationId)
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
