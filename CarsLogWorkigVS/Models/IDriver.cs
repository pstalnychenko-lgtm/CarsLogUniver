using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public interface IDriver : IUser
    {
        string FullNameByDriver { get; }
        string PhoneByDriver { get; }
        BloodType BloodType { get; }
        string LicenseNumber { get; }
        string LicenseIssuedBy { get; } // Орган, що видав водійське посвідчення
        DateTime LicenseExpiryDate { get; } // Дата закінчення терміну дії водійського посвідчення
        string DateOfLicenseFormatted { get; }
        bool MedicalCertStatus { get; } // Статус медичної довідки (true - дійсна, false - недійсна)
        List<LicenseCategory> LicenseCategories { get; } // Категорії водійських прав, які має водій

        void AddLicenseCategory(LicenseCategory category); // Метод для додавання категорії водійських прав
        bool IsLicenseValid(); // Метод для перевірки дійсності водійських прав
                               // (перевіряє, чи не закінчився термін дії та чи є медична довідка дійсною)
    }
}
