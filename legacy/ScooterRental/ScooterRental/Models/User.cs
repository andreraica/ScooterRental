using System;

namespace ScooterRental.Models
{
    public class User
    {
        public User(string PassportNumber)
        {
            if (PassportNumber == null)
            {
                throw new ApplicationException("You cannot have an user without passport number");
            }

            this.PassportNumber = PassportNumber;
        }

        public string PassportNumber { get; private set; }
    }
}