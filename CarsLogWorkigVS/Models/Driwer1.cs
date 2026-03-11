using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class Driver1 : User1
    {
        private string _fullName;
        
        public DateTime LicenseExpiryDate { get; set; }
        private bool MedicalCertStatus { get; set; }

        private UserSex DriverSex { get; set; }
    }
 }
