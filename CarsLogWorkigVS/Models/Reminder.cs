using System;

namespace CarsLogWorkig.Models
{
    public class VehicleComponent // Клас для зберігання інформації про компоненти автомобіля, які потребують заміни або обслуговування
    {
        private string _partName = string.Empty;
        public string PartName// Назва компонента (наприклад, "Гальмівні колодки", "Масляний фільтр" тощо)
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

        private uint _installationMileage;
        public uint InstallationMileage// Пробіг автомобіля на момент встановлення компонента
        {
            get => _installationMileage;
            private set => _installationMileage = value;
        }

        private bool _isExpired; 
        public bool IsExpired// Чи потребує заміни (t or f)
        {
            get => _isExpired;
            private set => _isExpired = value;
        }

        private DateTime _installationDate;
        public DateTime InstallationDate// Дата встановлення компонента
        {
            get => _installationDate;
            private set => _installationDate = value;
        }

        public VehicleComponent(string partName, uint installationMileage,
                                 bool isExpired, DateTime installationDate) // конструктор для створення запису про компонент автомобіля
        {
            PartName = partName;
            InstallationMileage = installationMileage;
            IsExpired = isExpired;
            InstallationDate = installationDate;
        }
    }
}
