using System;

namespace CarsLogWorkig.Models
{
    public class ServiceStation
    {
        private readonly Guid _id = Guid.NewGuid(); 
        public Guid Id => _id;

        private string _serviceName = string.Empty;
        public string ServiceName
        {
            get => _serviceName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назва сервісної станції не може бути порожньою."); 
                if (value.Trim().Length > 100)
                    throw new ArgumentException("Назва не може перевищувати 100 символів."); 
                _serviceName = value.Trim(); 
            }
        }

        private string _serviceAddress = string.Empty;
        public string ServiceAddress
        {
            get => _serviceAddress;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Адреса сервісної станції не може бути порожньою."); 
                if (value.Trim().Length > 200)
                    throw new ArgumentException("Адреса не може перевищувати 200 символів."); 
                _serviceAddress = value.Trim(); 
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

        public ServiceStation(string serviceName, string serviceAddress, string workingHours)
        {
            ServiceName = serviceName;
            ServiceAddress = serviceAddress;
            WorkingHours = workingHours;
        }

        public override string ToString() =>
            $"[Сервіс] {_serviceName} | {_serviceAddress} | Години: {_workingHours}";
    }
}
