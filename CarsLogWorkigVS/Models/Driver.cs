using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Driver : User, IDriver
    {
        private string _fullNameByDriver;
        public string FullNameByDriver // ім'я водія
        {
            get => _fullNameByDriver;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _fullNameByDriver = value;
            }
        }

        private string _phoneByDriver;
        public string PhoneByDriver // номер телефону
        {
            get => _phoneByDriver;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _phoneByDriver = value;
            }
        }

        private string _licenseNumber;
        public string LicenseNumber // номер ліцензії
        {
            get => _licenseNumber;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _licenseNumber = value;
            }
        }

        private string _licenseIssuedBy;
        public string LicenseIssuedBy // орган, що видав водійське посвідчення
        {
            get => _licenseIssuedBy;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _licenseIssuedBy = value;
            }
        }

        public BloodType BloodType { get; private set; } // група крові водія

        public DateTime LicenseExpiryDate { get; private set; } // дата закінчення терміну дії водійського посвідчення
        public string DateOfLicenseFormatted => LicenseExpiryDate.ToString("dd.MM.yyyy");

        public bool MedicalCertStatus { get; private set; } // статус наявності медичної довідки (true - є, false - немає)

        public List<LicenseCategory> LicenseCategories { get; private set; } = new List<LicenseCategory>();

        public Driver(string fullName, string phone, string licenseNumber, string licenseIssuedBy,
                      DateTime licenseExpiryDate, bool medicalCertStatus, BloodType bloodType) // конструктор для створення об'єкта водія
        {
            FullNameByDriver = fullName;
            PhoneByDriver = phone;
            LicenseNumber = licenseNumber;
            LicenseIssuedBy = licenseIssuedBy;
            LicenseExpiryDate = licenseExpiryDate;
            MedicalCertStatus = medicalCertStatus;
            BloodType = bloodType;
        }

        // Дії водія

        public void AddLicenseCategory(LicenseCategory category) // Додати категорію до посвідчення
        {
            if (category != null && !LicenseCategories.Contains(category))
                LicenseCategories.Add(category);
        }

        public bool IsLicenseValid() // Перевірити чи посвідчення ще дійсне
        {
            return LicenseExpiryDate > DateTime.Now;
        }
    }

    public enum BloodType // тип крові та резус
    {
        A_Positive,
        A_Negative,
        B_Positive,
        B_Negative,
        AB_Positive,
        AB_Negative,
        O_Positive,
        O_Negative
    }
}
