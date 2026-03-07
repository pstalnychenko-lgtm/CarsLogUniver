using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class Document 
      
      {
        private string _title;

        public Guid Id { get; private set; }
        public string Title
        {
            get => _title;
            set => _title = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException() : value;
        }
        public string PolicyNumber { get; set; }
        
        public DateTime ExpiryDate { get; set; }
        
        public string ImagePath { get; set; }
    }
}

