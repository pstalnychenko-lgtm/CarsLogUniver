using CarsLogWorkig.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogDrive.Models
{
    public class Admin : User1  
    {
        private string _fullName;

        private string Position { get; set; }
        private string ContactInfo { get; set; }


    }
}
