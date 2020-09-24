namespace ScooterRental.Domain.Models
{
    using System;

    public class Scooter
    {
        public Scooter(long id, User user = null)
        {
            this.Id = id;
            this.User = user;
        }

        public long Id { get; private set; }

        public bool Available => User == null;

        public User User { get; private set; }

        public void Rent(string passport)
        {
            if (this.User?.PassportNumber != null)
            {
                throw new ApplicationException("You cannot rent a rented scooter");
            }

            this.User = new User(passport);
        }

        public void TurnBack()
        {
            if (this.User?.PassportNumber == null)
            {
                throw new ApplicationException("You cannot turn back an unrented scooter");
            }

            this.User = null;
        }
    }
}
