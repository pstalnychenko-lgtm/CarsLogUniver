using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class ServiceRecord
    {
        
        private DateTime _dateOfLastServiceRec { get; set; }

        private decimal _value;
        private decimal _valueOfService
        {
            get => _value;
            set => _value = value < 0 ? 0 : value;
        }
        
    }
}