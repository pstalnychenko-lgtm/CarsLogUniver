using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class FuelEntry
    {
        private double _volume;

        public Guid Id { get; private set; }
        public double Volume
        {
            get => _volume;
            set => _volume = value <= 0 ? throw new ArgumentException() : value;
        }
        public string GasStation { get; set; }
        public bool IsFullTank { get; set; }
        public DateTime Date { get; private set; }

      
        
    }
}
