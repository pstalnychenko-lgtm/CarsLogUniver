using System;

namespace CarsLogWorkig.Models
{
    public class VehicleComponent
    {
        private string _partName;// Назва компонента (наприклад, "Гальмівні колодки", "Масляний фільтр" тощо)
        public string PartName
        {
            get => _partName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _partName = value;
            }
        }

        private uint _installationMileage;// Пробіг автомобіля на момент встановлення компонента
        public uint InstallationMileage
        {
            get => _installationMileage;
            private set => _installationMileage = value;
        }

        private bool _isExpired; // Чи потребує заміни (t or f)
        public bool IsExpired
        {
            get => _isExpired;
            private set => _isExpired = value;
        }

        private DateTime _installationDate;// Дата встановлення компонента
        public DateTime InstallationDate
        {
            get => _installationDate;
            private set => _installationDate = value;
        }

        public VehicleComponent(string partName, uint installationMileage,
                                 bool isExpired, DateTime installationDate)
        {
            PartName = partName;
            InstallationMileage = installationMileage;
            IsExpired = isExpired;
            InstallationDate = installationDate;
        }
    }
}
