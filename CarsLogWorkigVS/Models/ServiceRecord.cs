using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class ServiceRecord
    {
        
        private DateTime DateOfLastServiceRec { get; set; }

        private decimal _value;
        private decimal ValueOfService
        {
            get => _value;
            set => _value = value < 0 ? 0 : value;
        }
        
    }
}