using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
        
    public interface IDriver : IUser // Інтерфейс для водія: доступ до власних даних посвідчення, категорій, медичної довідки
    {
        
        string FullNameByDriver { get; }      // Переглянути повне ім'я
        string PhoneByDriver { get; }         // Переглянути номер телефону
        BloodType BloodType { get; }          // Переглянути групу крові

        
        string LicenseNumber { get; }         // Переглянути номер посвідчення
        string LicenseIssuedBy { get; }       // Переглянути орган, що видав
        DateTime LicenseExpiryDate { get; }   // Переглянути дату закінчення
        string DateOfLicenseFormatted { get; }// Відформатована дата закінчення посвідчення

        
        bool MedicalCertStatus { get; }       // Переглянути статус медичної довідки
        List<LicenseCategory> LicenseCategories { get; } // Переглянути категорії посвідчення

        
        void AddLicenseCategory(LicenseCategory category); // Додати категорію до посвідчення
        bool IsLicenseValid();                             // Перевірити чи посвідчення дійсне
    }
}
