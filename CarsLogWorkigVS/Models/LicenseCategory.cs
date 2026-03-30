using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class LicenseCategory
    {
        public Guid Id { get; init; } = Guid.NewGuid();// унікальний ідентифікатор категорії водійського посвідчення

        public DateTime DateOfIssue { get; private set; } // Дата видачі категорії
        public string DateOfIssueFormatted => DateOfIssue.ToString("dd.MM.yyyy");

        public void VerifyCanDrive(CategoryName vehicleCategory)
        {
            if (this.CategoryName != vehicleCategory)
            {
                throw new InvalidOperationException($"У вас немає права керувати транспортом категорії {vehicleCategory}");
            }
        }
        public DateTime ExpirationDate { get; private set; }// Дата закінчення терміну дії категорії
        public string ExpirationDateFormatted => ExpirationDate.ToString("dd.MM.yyyy");


        // Initialize backing fields to non-null defaults to satisfy nullable analysis
        private string _cityOfIssue = string.Empty;
        public string CityOfIssue // місто в якому видано
        {
            get => _cityOfIssue;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _cityOfIssue = value;
            }
        }

        private string _serialNumber = string.Empty;
        public string SerialNumber
        {
            get => _serialNumber;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _serialNumber = value;
            }
        }

        private string _trafficPoliceCenter = string.Empty;
        public string TrafficPoliceCenter // Центр ДАІ, який видав категорію
        {
            get => _trafficPoliceCenter;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _trafficPoliceCenter = value;
            }
        }

        public CategoryName CategoryName { get; private set; }

        public LicenseCategory(DateTime dateOfIssue, DateTime expirationDate,
                                string cityOfIssue, string serialNumber, string trafficPoliceCenter)/* конструктор для створення
                                                                                                     * категорії водійського посвідчення*/
        {
            DateOfIssue = dateOfIssue;
            ExpirationDate = expirationDate;
            CityOfIssue = cityOfIssue;
            SerialNumber = serialNumber;
            TrafficPoliceCenter = trafficPoliceCenter;
        }
    }

    public enum CategoryName
    {
        A=1,
        A1=2,
        B=3,
        B1=4,
        C=5,
        C1=6,
        D=7,
        D1=8,
        CE=9,
        C1E=10,
        BE=11,
        T1=12,
        T2=13
    }
}
