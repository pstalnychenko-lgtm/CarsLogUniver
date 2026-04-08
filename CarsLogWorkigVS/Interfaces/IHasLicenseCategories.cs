using CarsLogWorkig.Models;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasLicenseCategories
    {
        List<LicenseCategory> LicenseCategories { get; }
        void AddLicenseCategory(LicenseCategory category); 
        void RemoveLicenseCategory(LicenseCategory category);
    }
}
