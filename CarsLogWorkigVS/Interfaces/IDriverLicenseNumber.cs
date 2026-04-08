using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IDriverLicenseNumber
    {
        string LicenseNumber { get; }
        string LicenseIssuedBy { get; }

        void SetLicenseNumber(string value);
        void SetLicenseIssuedBy(string value);
        void ChangeLicenseNumber(string newLicenseNumber);
        void ChangeLicenseIssuedBy(DateTime value);
    }
}
