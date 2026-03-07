using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class ServiceRecord
    {
        public Guid Id { get; private set; }
        
        public DateTime Date { get; private set; }
        
        public int Mileage { get; set; }
        
        public decimal TotalCost { get; set; }
        
        public List<string> PartsChanged { get; set; }
        
        public string WarrantyPeriod { get; set; }

        
    }
}