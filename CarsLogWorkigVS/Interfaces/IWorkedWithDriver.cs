using CarsLogWorkig.Models;
using System;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IWorkedWithDriver
    {
        void ChangeBloodType(BloodType newBloodType); 
        List<BloodType> GetCompatibleDonorTypes(); 
        void ChangeLicenseNumber(string newLicenseNumber); 
        void ChangeLicenseIssuedBy(string newIssuedBy); 
        void ChangeLicenseExpiryDate(DateTime newExpiryDate); 
        void SetLicenseExpiryDate(DateTime expiryDate); 
        List<LicenseCategory> LicenseCategories { get; }
        void AddLicenseCategory(LicenseCategory category); 
        void RemoveLicenseCategory(LicenseCategory category); 
        void UpdateDriverInfo(string info); 
    }
}
