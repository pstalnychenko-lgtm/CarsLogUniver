using System;

namespace CarsLogWorkig.Models
{
    public class ServiceStation
    {
        public Guid Id { get; init; } = Guid.NewGuid();// унікальний ідентифікатор сервісної станції

        private string _serviseName = string.Empty;// назва сервісної станції
        public string ServiseName
        {
            get => _serviseName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _serviseName = value;
            }
        }

        private string _serviseAddress = string.Empty;
        public string ServiseAddress //сервісний адрес
        {
            get => _serviseAddress;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _serviseAddress = value;
            }
        }

        private string _workingHours = string.Empty;
        public string WorkingHours // час роботи
        {
            get => _workingHours;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _workingHours = value;
            }
        }

        public ServiceStation(string serviseName, string serviseAddress, string workingHours) // категорії сервісної станції
        {
            ServiseName = serviseName;
            ServiseAddress = serviseAddress;
            WorkingHours = workingHours;
        }
    }
}
