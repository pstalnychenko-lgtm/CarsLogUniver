using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class Owner : User1
    {
        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set => _fullName = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException() : value;
        }

    }
}
