using System;

namespace ScooterRental.Models
{
    public class Monitor
    {
        public Monitor(Scooter scooter, int locationAreaId)
        {
            if(scooter.User?.PassportNumber == null)
            {
                throw new ApplicationException("You cannot monitor an unrented scooter");
            }

            if (locationAreaId <= 0)
            {
                throw new ApplicationException("You cannot monitor without a location area");
            }

            this.Scooter = scooter;
            this.LocationAreaId = locationAreaId;
        }
        public int Id { get; set; }

        public Scooter Scooter { get; private set; }
        public int LocationAreaId { get; private set; }

        public void SetLocationArea(int locationAreaId)
        {
            this.LocationAreaId = locationAreaId;
        }
    }
}