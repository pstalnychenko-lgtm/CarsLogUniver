using System;

namespace CarsLogWorkig.Models
{
    public class TripLog // Клас для запису поїздок
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public DateTime TripDate { get; private set; }

        private string _departurePoint;
        public string DeparturePoint // Можна додати окремо місто та адресу, якщо потрібно
        {
            get => _departurePoint;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _departurePoint = value;
            }
        }

        private string _destination;
        public string Destination
        {
            get => _destination;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _destination = value;
            }
        }

        public TripPurpose Purpose { get; private set; } // Мета поїздки (наприклад, особиста, службова, обслуговування тощо)

        public uint StartMileage { get; private set; }

        public uint EndMileage { get; private set; }

        // Обчислювана відстань поїздки
        public uint DistanceKm => EndMileage > StartMileage
            ? EndMileage - StartMileage
            : 0;

        private string _notes;
        public string Notes
        {
            get => _notes;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _notes = value;
            }
        }

        public TripLog(DateTime tripDate, string departurePoint, string destination,
                        TripPurpose purpose, uint startMileage, uint endMileage, string notes)
        {
            TripDate = tripDate;
            DeparturePoint = departurePoint;
            Destination = destination;
            Purpose = purpose;
            StartMileage = startMileage;
            EndMileage = endMileage;
            Notes = notes;
        }
    }

    public enum TripPurpose
    {
        Personal,
        Business,
        Service,
        Other
    }
}
