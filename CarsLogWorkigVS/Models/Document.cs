using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class Document 
      
      {
        private string _title;
        private string PolicyNumber { get; set; }
        
        public DateTime ExpiryDate { get; set; }
        
        private string ImagePath { get; set; }
        
    }
}

