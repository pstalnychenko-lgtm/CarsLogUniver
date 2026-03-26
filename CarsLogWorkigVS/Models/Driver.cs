using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Driver : User, IDriver
    {
        private string _fullNameByDriver;
        public string FullNameByDriver
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
        public string PhoneByDriver
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
        public string LicenseNumber
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
        public string LicenseIssuedBy
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

        public BloodType BloodType { get; private set; }

        public DateTime LicenseExpiryDate { get; private set; }
        public string DateOfLicenseFormatted => LicenseExpiryDate.ToString("dd.MM.yyyy");

        public bool MedicalCertStatus { get; private set; }

        public List<LicenseCategory> LicenseCategories { get; private set; } = new List<LicenseCategory>();

        public Driver(string fullName, string phone, string licenseNumber, string licenseIssuedBy,
                      DateTime licenseExpiryDate, bool medicalCertStatus, BloodType bloodType)
        {
            FullNameByDriver = fullName;
            PhoneByDriver = phone;
            LicenseNumber = licenseNumber;
            LicenseIssuedBy = licenseIssuedBy;
            LicenseExpiryDate = licenseExpiryDate;
            MedicalCertStatus = medicalCertStatus;
            BloodType = bloodType;
        }

        public void AddLicenseCategory(LicenseCategory category) // метод для додавання категорії водійських прав
        {
            if (category != null && !LicenseCategories.Contains(category))
                LicenseCategories.Add(category);
        }

        public bool IsLicenseValid() // метод для перевірки дійсності водійських прав
        {
            return LicenseExpiryDate > DateTime.Now;
        }
    }

    public enum BloodType //перелік груп крові для водія
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
