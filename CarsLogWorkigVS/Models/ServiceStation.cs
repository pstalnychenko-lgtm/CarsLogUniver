using System;

namespace CarsLogWorkig.Models
{
    public class ServiceStation
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Address { get; set; }

        public string WorkingHours { get; set; }
    }
}
