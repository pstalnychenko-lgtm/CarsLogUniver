using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class ServiceRecord
    {
        public Guid Id { get; private set; }
        
        public DateTime Date { get; private set; }

        private decimal _value;
        public decimal Value
        {
            get => _value;
            set => _value = value < 0 ? 0 : value;
        }

        public List<string> PartsChanged { get; set; }
        
        public string WarrantyPeriod { get; set; }

        
    }
}