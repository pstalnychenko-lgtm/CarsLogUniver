using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class Document 
      
      {
        private string _title;
        private string _policyNumber { get; set; }
        
        public DateTime ExpiryDate { get; set; }
        
        private string _imagePath { get; set; }
        
    }
}

