using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class LicenseCategory
    {
        public Guid Id { get; init; } = Guid.NewGuid();// унікальний ідентифікатор категорії водійського посвідчення

        public DateTime DateOfIssue { get; private set; } // Дата видачі категорії
        public string DateOfIssueFormatted => DateOfIssue.ToString("dd.MM.yyyy");


        public DateTime ExpirationDate { get; private set; }// Дата закінчення терміну дії категорії
        public string ExpirationDateFormatted => ExpirationDate.ToString("dd.MM.yyyy");


        private string _cityOfIssue;
        public string CityOfIssue
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

        private string _serialNumber;
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

        private string _trafficPoliceCenter;
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
        A,
        A1,
        B,
        B1,
        C,
        C1,
        D,
        D1,
        CE,
        C1E,
        BE,
        T1,
        T2
    }
}
