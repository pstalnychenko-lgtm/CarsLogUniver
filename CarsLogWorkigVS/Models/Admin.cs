using System;

namespace CarsLogWorkig.Models
{
    public class Admin : User
    {
        public string FullName { get; set; }

        public string Position { get; set; }

        public string ContactInfo { get; set; }
    }
}
