using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class FuelEntry
    {
        private string _gasStation { get; set; }
        
        private FuelsType TypeOfFuel ;

    }

    public enum FuelsType
    {
        Petrol,
        Diesel,
        Electric,
        Hybrid
    }
}
