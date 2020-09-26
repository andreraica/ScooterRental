namespace ScooterRental.Domain.Interfaces.Data.Repositories
{
    using ScooterRental.Domain.Models;
    
    public interface ITrackingRepository
    {
        Tracking GetByScooter(int scooterId);
        void Insert(Tracking tracking);
    }
}
