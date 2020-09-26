namespace ScooterRental.Domain.Interfaces.Services
{
    using ScooterRental.Domain.Models;

    public interface ITrackingService
    {
        Tracking GetByScooterId(int scooterId);
        void Insert(int scooterId, int locationId);
    }
}
