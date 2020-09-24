namespace ScooterRental.Domain.Interfaces.Data.Repositories
{
    using ScooterRental.Domain.Models;
    
    public interface IScooterRepository
    {
        Scooter Get(int id);
        void Update(Scooter scooter);
    }
}
