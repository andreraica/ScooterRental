namespace ScooterRental.Domain.Interfaces.Services
{
    using ScooterRental.Domain.Models;

    public interface IScooterService
    {
        Scooter Rent(int scooterId, string passportNumber);

        Scooter TurnBack(int scooterId);
    }
}
