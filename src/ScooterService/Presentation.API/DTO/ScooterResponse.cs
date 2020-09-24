namespace ScooterRental.Presentation.ScooterRental.WebAPI.DTO
{
    public class ScooterResponse
    {
        public ScooterResponse(int id)
        {
            this.Id = id;
        }

        public int Id { get; private set; }
    }
}
