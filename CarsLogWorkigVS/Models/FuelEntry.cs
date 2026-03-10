using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class FuelEntry
    {
        private double _volume;

        public Guid Id { get; private set; }
        
        public string GasStation { get; set; }
        public bool IsFullTank { get; set; }
        public DateTime Date { get; private set; }

      
        
    }
}
