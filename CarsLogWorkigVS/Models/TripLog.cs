using System;

namespace CarsLogWorkig.Models
{
    public class TripLog // Клас для запису поїздок
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public DateTime TripDate { get; set; }

        public string DeparturePoint { get; set; }// Можна додати окремо місто та адресу, якщо потрібно

        public string Destination { get; set; }

        public TripPurpose Purpose { get; set; }// Мета поїздки (наприклад, особиста, службова, обслуговування тощо)

        public uint StartMileage { get; set; }

        public uint EndMileage { get; set; }

        // Обчислювана відстань поїздки
        public uint DistanceKm => EndMileage > StartMileage
            ? EndMileage - StartMileage
            : 0;

        public string Notes { get; set; }
    }
    public enum TripPurpose
    {
        Personal,
        Business,
        Service,
        Other
    }
}
