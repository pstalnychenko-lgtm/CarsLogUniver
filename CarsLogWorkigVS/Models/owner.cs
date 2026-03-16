using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Owner : User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime DateOfPurchaseTheCar { get;private set ; } // дата покупки автомобиля
    }
}
