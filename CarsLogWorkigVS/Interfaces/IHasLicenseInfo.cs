using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasLicenseInfo
    {
        string LicenseNumber { get; }
        string LicenseIssuedBy { get; }
        DateTime LicenseExpiryDate { get; }
        string DateOfLicenseFormatted { get; }
    }
}
