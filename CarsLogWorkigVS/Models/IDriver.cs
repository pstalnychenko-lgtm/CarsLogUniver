using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public interface IDriver : IUser
    {
        BloodType BloodType { get; }
        string LicenseNumber { get; }
        string LicenseIssuedBy { get; }
        DateTime LicenseExpiryDate { get; }
        string DateOfLicenseFormatted { get; }
        bool MedicalCertStatus { get; }
        List<LicenseCategory> LicenseCategories { get; }

        void AddLicenseCategory(LicenseCategory category);
        bool IsLicenseValid();
        string GetDriverInfo();
        string GetBloodType();
    }
}
