using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Driver : User
    {
        private string _fullName;
        public string FullName
        {
            get => _fullName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _fullName = value;
            }
        }

        private string _phone;
        public string Phone // номер телефону
        {
            get => _phone;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _phone = value;
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

        public DateTime LicenseExpiryDate { get; private set; } // дата закінчення терміну дії водійського посвідчення

        public bool MedicalCertStatus { get; private set; }

        public List<LicenseCategory> LicenseCategories { get; private set; } = new List<LicenseCategory>();

        public Driver(string fullName, string phone, string licenseNumber, string licenseIssuedBy,
                      DateTime licenseExpiryDate, bool medicalCertStatus)
        {
            FullName = fullName;
            Phone = phone;
            LicenseNumber = licenseNumber;
            LicenseIssuedBy = licenseIssuedBy;
            LicenseExpiryDate = licenseExpiryDate;
            MedicalCertStatus = medicalCertStatus;
        }
    }
}
