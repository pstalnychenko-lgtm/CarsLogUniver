using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Driver : User
    {
        public string FullName { get; set; }

        public string Phone { get; set; }// номер телефону

        public string LicenseNumber { get; set; }

        public string LicenseIssuedBy { get; set; }// орган, що видав водійське посвідчення

        public DateTime LicenseExpiryDate { get; set; }// дата закінчення терміну дії водійського посвідчення

        public bool MedicalCertStatus { get; set; }

        public List<LicenseCategory> LicenseCategories { get; set; } = new List<LicenseCategory>();
    }
}
