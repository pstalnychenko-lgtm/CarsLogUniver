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
        public string Destination// Можна додати окремо місто та адресу, якщо потрібно
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

        public uint StartMileage { get; private set; }// Пробіг на початку поїздки 

        public uint EndMileage { get; private set; }// Пробіг в кінці поїздки



        private uint _distanceKm; //додаткова змінна для зберігання відстані

        public uint DistanceKm// Обчислювана відстань поїздкb
        {
            get => _distanceKm;
            private set
            {
                if (value == 0)
                    return;
                else
                    _distanceKm = value;
            }
        }

        public TripLog(DateTime tripDate, string departurePoint, string destination,
                        TripPurpose purpose, uint startMileage, uint endMileage, string notes) // канструктор для створення запису про поїздку
        {
            TripDate = tripDate;
            DeparturePoint = departurePoint;
            Destination = destination;
            Purpose = purpose;
            StartMileage = startMileage;
            EndMileage = endMileage;
            
        }
    }

    public enum TripPurpose// Перелік можливих цілей поїздки
    {
        Personal,
        Business,
        Service,
        Other
    }
}
