using System;

using System.Collections.Generic;

using System.Text;

namespace CarsLogWorkig.Models
{
    public class Note
    {
        private string _content;

        public Guid Id { get; private set; }
        public string Content
        {
            get => _content;
            set => _content = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException() : value;
        }
        public DateTime CreatedAt { get; private set; }

      
    }
}
