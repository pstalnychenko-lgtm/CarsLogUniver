using CarsLogWorkigVS.Interfaces;
using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Driver : User,
        IHasLicenseInfo,
        IHasLicenseCategories,
        IValidatesLicense,
        IHasBloodType,
        IProvidesDriverInfo
    {
        private string _licenseNumber = string.Empty;
        public string LicenseNumber => _licenseNumber;

        private void SetLicenseNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Номер посвідчення не може бути порожнім.");
            _licenseNumber = value.Trim();
        }

        private string _licenseIssuedBy = string.Empty;
        public string LicenseIssuedBy => _licenseIssuedBy;

        private void SetLicenseIssuedBy(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Орган видачі посвідчення не може бути порожнім.");
            _licenseIssuedBy = value.Trim();
        }

        private BloodType _bloodType;
        public BloodType BloodType => _bloodType;

        public DateTime LicenseExpiryDate { get; private set; }
        public string DateOfLicenseFormatted => LicenseExpiryDate.ToString("dd.MM.yyyy");

        private bool _medicalCertStatus;
        public bool MedicalCertStatus => _medicalCertStatus;

        public List<LicenseCategory> LicenseCategories { get; private set; } = new List<LicenseCategory>();

        public Driver(string firstName, string lastName, string phone,
                      string licenseNumber, string licenseIssuedBy,
                      DateTime licenseExpiryDate, bool medicalCertStatus, BloodType bloodType)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            SetLicenseNumber(licenseNumber);
            SetLicenseIssuedBy(licenseIssuedBy);
            LicenseExpiryDate = licenseExpiryDate;
            _medicalCertStatus = medicalCertStatus;
            _bloodType = bloodType;
        }

        public void ChangeBloodType(BloodType newBloodType)
        {
            if (_bloodType == newBloodType)
                throw new ArgumentException("Ця група крові вже встановлена.");
            _bloodType = newBloodType;
        }

        public List<BloodType> GetCompatibleDonorTypes()
        {
            return _bloodType switch
            {
                BloodType.O_Negative   => new List<BloodType> { BloodType.O_Negative },
                BloodType.O_Positive   => new List<BloodType> { BloodType.O_Negative, BloodType.O_Positive },
                BloodType.A_Negative   => new List<BloodType> { BloodType.O_Negative, BloodType.A_Negative },
                BloodType.A_Positive   => new List<BloodType> { BloodType.O_Negative, BloodType.O_Positive, BloodType.A_Negative, BloodType.A_Positive },
                BloodType.B_Negative   => new List<BloodType> { BloodType.O_Negative, BloodType.B_Negative },
                BloodType.B_Positive   => new List<BloodType> { BloodType.O_Negative, BloodType.O_Positive, BloodType.B_Negative, BloodType.B_Positive },
                BloodType.AB_Negative  => new List<BloodType> { BloodType.O_Negative, BloodType.A_Negative, BloodType.B_Negative, BloodType.AB_Negative },
                BloodType.AB_Positive  => new List<BloodType>
                {
                    BloodType.O_Negative, BloodType.O_Positive,
                    BloodType.A_Negative, BloodType.A_Positive,
                    BloodType.B_Negative, BloodType.B_Positive,
                    BloodType.AB_Negative, BloodType.AB_Positive
                },
                _ => new List<BloodType>()
            };
        }

        public void AddLicenseCategory(LicenseCategory category)
        {
            if (category == null)
                throw new ArgumentNullException("Категорія не може бути порожньою.");
            if (!LicenseCategories.Contains(category))
                LicenseCategories.Add(category);
        }

        public bool IsLicenseValid() => LicenseExpiryDate > DateTime.Now;

        public string GetDriverInfo()
        {
            return $"Водій: {FullName}, Телефон: {Phone}, Посвідчення: {_licenseNumber}, " +
                   $"Видане: {_licenseIssuedBy}, Дійсне до: {DateOfLicenseFormatted}, " +
                   $"Мед. довідка: {_medicalCertStatus}, Група крові: {_bloodType}";
        }

        public string GetBloodType() => _bloodType.ToString();

        public override string ToString() =>
            $"[Водій] {FullName} | Посвідчення: {_licenseNumber} | Дійсне до: {DateOfLicenseFormatted} | Мед. довідка: {_medicalCertStatus}";
    }

    public enum BloodType
    {
        A_Positive, A_Negative,
        B_Positive, B_Negative,
        AB_Positive, AB_Negative,
        O_Positive, O_Negative
    }
}
