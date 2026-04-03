using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    
    public class Driver : User, IDriver
    {
        private string _licenseNumber = string.Empty;
        public string LicenseNumber
        {
            get => _licenseNumber;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _licenseNumber = value;
            }
        }

        private string _licenseIssuedBy = string.Empty;
        public string LicenseIssuedBy
        {
            get => _licenseIssuedBy;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _licenseIssuedBy = value;
            }
        }

        public BloodType BloodType { get; private set; }
        public DateTime LicenseExpiryDate { get; private set; }
        public string DateOfLicenseFormatted => LicenseExpiryDate.ToString("dd.MM.yyyy");
        public bool MedicalCertStatus { get; private set; }
        public List<LicenseCategory> LicenseCategories { get; private set; } = new List<LicenseCategory>();

        public Driver(string firstName, string lastName, string phone,
                      string licenseNumber, string licenseIssuedBy,
                      DateTime licenseExpiryDate, bool medicalCertStatus, BloodType bloodType)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            LicenseNumber = licenseNumber;
            LicenseIssuedBy = licenseIssuedBy;
            LicenseExpiryDate = licenseExpiryDate;
            MedicalCertStatus = medicalCertStatus;
            BloodType = bloodType;
        }

        public void AddLicenseCategory(LicenseCategory category)
        {
            if (category != null && !LicenseCategories.Contains(category))
                LicenseCategories.Add(category);
        }

        public bool IsLicenseValid()
        {
            return LicenseExpiryDate > DateTime.Now;
        }

        public string GetDriverInfo()
        {
            return $"Driver: {FullName}, Phone: {Phone}, License: {LicenseNumber}, " +
                   $"Issued By: {LicenseIssuedBy}, Expiry Date: {DateOfLicenseFormatted}, " +
                   $"Medical Cert Valid: {MedicalCertStatus}, Blood Type: {BloodType}";
        }

        public string GetBloodType()
        {
            return BloodType.ToString();
        }
    }

    public enum BloodType
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
