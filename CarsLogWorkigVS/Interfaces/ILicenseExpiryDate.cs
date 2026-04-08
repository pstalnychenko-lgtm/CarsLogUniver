using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface ILicenseExpiryDate
    {
        DateTime LicenseExpiryDate { get; }
        string DateOfLicenseFormatted { get; }
        bool IsLicenseValid();

        void SetLicenseExpiryDate(DateTime expiryDate);
        void ChangeLicenseExpiryDate(DateTime newExpiryDate);
    }
}
