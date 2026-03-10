using CarsLogWorkig.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogDrive.Models
{
    public class Admin : User1  
    {
        private string _fullName;

        public string Position { get; set; }
        public string ContactInfo { get; set; }


    }
}
