using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class LicenseCategory
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public DateTime DateOfIssue { get; set; }// Дата видачі категорії

        public DateTime ExpirationDate { get; set; }

        public string CityOfIssue { get; set; }

        public string SerialNumber { get; set; }

        public string TrafficPoliceCenter { get; set; } // Центр ДАІ, який видав категорію
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
