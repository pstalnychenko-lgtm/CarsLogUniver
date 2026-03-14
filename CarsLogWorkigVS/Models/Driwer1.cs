using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class Driver1 : User1
    {
        private string _fullName;
        
        public DateTime LicenseExpiryDate { get; set; }
        private bool _medicalCertStatus { get; set; }

        private UserSex _driverSex { get; set; }
    }
 }
