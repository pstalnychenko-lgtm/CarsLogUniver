using System;

namespace CarsLogWorkig.Models
{
    public class ServiceStation
    {
        public Guid Id { get; init; } = Guid.NewGuid();// унікальний ідентифікатор сервісної станції

        private string _serviseName;// назва сервісної станції
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

        private string _serviseAddress;
        public string ServiseAddress
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

        private string _workingHours;
        public string WorkingHours
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

        public ServiceStation(string serviseName, string serviseAddress, string workingHours)
        {
            Name = serviseName;
            Address = serviseAddress;
            WorkingHours = workingHours;
        }
    }
}
