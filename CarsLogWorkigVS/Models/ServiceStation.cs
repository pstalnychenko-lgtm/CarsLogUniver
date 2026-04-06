using System;

namespace CarsLogWorkig.Models
{
    public class ServiceStation
    {
        private readonly Guid _id = Guid.NewGuid();
        public Guid Id => _id;

        private string _serviseName = string.Empty;
        public string ServiseName
        {
            get => _serviseName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назва сервісної станції не може бути порожньою.");
                if (value.Trim().Length > 100)
                    throw new ArgumentException("Назва не може перевищувати 100 символів.");
                _serviseName = value.Trim();
            }
        }

        private string _serviseAddress = string.Empty;
        public string ServiseAddress
        {
            get => _serviseAddress;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Адреса сервісної станції не може бути порожньою.");
                if (value.Trim().Length > 200)
                    throw new ArgumentException("Адреса не може перевищувати 200 символів.");
                _serviseAddress = value.Trim();
            }
        }

        private string _workingHours = string.Empty;
        public string WorkingHours
        {
            get => _workingHours;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Години роботи не можуть бути порожніми.");
                _workingHours = value.Trim();
            }
        }

        public ServiceStation(string serviseName, string serviseAddress, string workingHours)
        {
            ServiseName = serviseName;
            ServiseAddress = serviseAddress;
            WorkingHours = workingHours;
        }

        public override string ToString() =>
            $"[Сервіс] {_serviseName} | {_serviseAddress} | Години: {_workingHours}";
    }
}
