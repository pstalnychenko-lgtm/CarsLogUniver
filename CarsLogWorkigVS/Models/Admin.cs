using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogDrive.Models1
{
    public class Admin 
    {
        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set => _fullName = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException() : value;
        }
        public string Position { get; set; }
        public string ContactInfo { get; set; }
    }
}
