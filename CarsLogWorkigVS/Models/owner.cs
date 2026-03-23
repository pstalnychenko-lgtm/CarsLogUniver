
using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Owner : User // клас для зберігання інформації про власника автомобіля, успадковує від класу User
    {
        private string _firstNameByOwner;
        public string FirstNameByOwner // властивість для зберігання імені власника автомобіля
        {
            get => _firstNameByOwner;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _firstNameByOwner = value;
            }
        }

        private string _lastNameByOwner;
        public string LastNameByOwner // властивість для зберігання прізвища власника автомобіля
        {
            get => _lastNameByOwner;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _lastNameByOwner = value;
            }
        }

        private string _phoneByOwner;
        public string PhoneByOwner //   властивість для зберігання номера телефону власника автомобіля
        {
            get => _phoneByOwner;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _phoneByOwner = value;
            }
        }

        private string _addressByOwner;
        public string AddressByOwner // властивість для зберігання адреси власника автомобіля
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
            DateTime dateOfPurchaseTheCar) // конструктор для створення запису про власника автомобіля

        {
            FirstNameByOwner = firstNameByOwnerv ;
            LastNameByOwner = lastNameByOwner ;
            PhoneByOwner = phoneByOwner ;
            AddressByOwner = addressByOwner;

        }
    }
}
