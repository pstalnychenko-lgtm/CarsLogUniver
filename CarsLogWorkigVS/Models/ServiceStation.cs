using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class ServiceStation 
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException() : value;
        }
        public string Address { get; set; }
        public string WorkSpecialization { get; set; }

        
    }
}
