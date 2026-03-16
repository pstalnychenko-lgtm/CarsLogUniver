using System;

namespace CarsLogWorkig.Models
{
    public class ServiceStation
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        private string _name;
        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _name = value;
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _address = value;
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

        public ServiceStation(string name, string address, string workingHours)
        {
            Name = name;
            Address = address;
            WorkingHours = workingHours;
        }
    }
}
