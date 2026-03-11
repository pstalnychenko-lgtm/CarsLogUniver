using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class FuelEntry
    {
        public string GasStation { get; set; }
        
        public FuelsType TypeOfFuel ;

    }

    public enum FuelsType
    {
        Petrol,
        Diesel,
        Electric,
        Hybrid
    }
}
