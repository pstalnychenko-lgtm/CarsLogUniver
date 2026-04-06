using System;

namespace CarsLogWorkig.Models
{
    public class TripLog
    {
        private readonly Guid _id = Guid.NewGuid();
        public Guid Id => _id;

        public DateTime TripDate { get; private set; }

        private string _departurePoint = string.Empty;
        public string DeparturePoint
        {
            get => _departurePoint;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Точка відправлення не може бути порожньою.");
                if (value.Trim().Length > 100)
                    throw new ArgumentException("Точка відправлення не може перевищувати 100 символів.");
                _departurePoint = value.Trim();
            }
        }

        private string _destination = string.Empty;
        public string Destination
        {
            get => _destination;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Пункт призначення не може бути порожнім.");
                if (value.Trim().Length > 100)
                    throw new ArgumentException("Пункт призначення не може перевищувати 100 символів.");
                _destination = value.Trim();
            }
        }

        public TripPurpose Purpose { get; private set; }

        private uint _startMileage;
        public uint StartMileage
        {
            get => _startMileage;
            private set => _startMileage = value;
        }

        private uint _endMileage;
        public uint EndMileage
        {
            get => _endMileage;
            private set
            {
                if (value < _startMileage)
                    throw new ArgumentException("Кінцевий пробіг не може бути меншим за початковий.");
                _endMileage = value;
            }
        }

        public uint DistanceKm => _endMileage - _startMileage;

        private string _notes = string.Empty;
        public string Notes
        {
            get => _notes;
            private set
            {
                if (value != null && value.Trim().Length > 1000)
                    throw new ArgumentException("Нотатки не можуть перевищувати 1000 символів.");
                _notes = value?.Trim() ?? string.Empty;
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

        public override string ToString() =>
            $"[{Purpose}] {_departurePoint} → {_destination} | {DistanceKm} км | {TripDate:dd.MM.yyyy}";
    }

    public enum TripPurpose { Personal, Business, Service, Other }
}
