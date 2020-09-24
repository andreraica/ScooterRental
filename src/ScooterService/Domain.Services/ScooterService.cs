namespace ScooterRental.Domain.Services
{
    using System;
    using ScooterRental.Domain.Interfaces.Data.Repositories;
    using ScooterRental.Domain.Interfaces.Services;
    using ScooterRental.Domain.Models;

    public class ScooterService : IScooterService
    {
        private readonly IScooterRepository scooterRepository;

        public ScooterService(IScooterRepository scooterRepository)
        {
            this.scooterRepository = scooterRepository;
        }

        public Scooter Rent(int scooterId, string passportNumber)
        {
            try
            {
                var scooter = this.scooterRepository.Get(scooterId);
                scooter.Rent(passportNumber);

                this.scooterRepository.Update(scooter);

                //Monitor
                //var locationAreaId = new Random().Next(1, 200);
                //monitorDal.Insert(new Monitor(scooter, locationAreaId));

                return scooter;
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }

        public Scooter TurnBack(int scooterId)
        {
            try
            {
                var scooter = this.scooterRepository.Get(scooterId);
                scooter.TurnBack();

                this.scooterRepository.Update(scooter);

                //Monitor
                //var locationAreaId = new Random().Next(1, 200);
                //monitorDal.Insert(new Monitor(scooter, locationAreaId));

                return scooter;
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }
    }
}
