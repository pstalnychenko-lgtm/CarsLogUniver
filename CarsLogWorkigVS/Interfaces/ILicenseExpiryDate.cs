using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface ILicenseExpiryDate
    {
        DateTime LicenseExpiryDate { get; }
        string DateOfLicenseFormatted { get; }

        void SetLicenseExpiryDate(DateTime expiryDate);
        void ChangeLicenseExpiryDate(DateTime newExpiryDate);
    }
}
