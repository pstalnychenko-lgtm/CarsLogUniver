using Android.Media.TV;
using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Owner : User
    {
        private string _firstNameByOwner;
        public string FirstNameByOwner
        {
            get => _firstNameByOwner;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _firstNameByOwner = value;
            }
        }

        private string _lastNameByOwner;
        public string LastNameByOwner
        {
            get => _lastNameByOwner;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _lastNameByOwner = value;
            }
        }

        private string _phoneByOwner;
        public string PhoneByOwner
        {
            get => _phoneByOwner;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _phoneByOwner = value;
            }
        }

        private string _addressByOwner;
        public string AddressByOwner
        {
            get => _addressByOwner;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _addressByOwner = value;
            }
        }

        public DateTime DateOfPurchaseTheCar { get;private set ; } 
        public string DateOfPurchaseTheCarFormatted => DateOfPurchaseTheCar.ToString("dd.MM.yyyy");// дата покупки автомобиля

        public Owner(string firstNameByOwnerv , string lastNameByOwner, string phoneByOwner, string addressByOwner,
            DateTime dateOfPurchaseTheCar)
         
        {
            FirstNameByOwner = firstNameByOwnerv ;
            LastNameByOwner = lastNameByOwner ;
            PhoneByOwner = phoneByOwner ;
            AddressByOwner = addressByOwner;

        }
    }
}
