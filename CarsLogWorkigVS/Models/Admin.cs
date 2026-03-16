using System;

namespace CarsLogWorkig.Models
{
    public class Admin : User
    {
        public string FullName { get;private set; }

        public string Position { get;private set; }

        public string ContactInfo { get;private set; }
    }
}
