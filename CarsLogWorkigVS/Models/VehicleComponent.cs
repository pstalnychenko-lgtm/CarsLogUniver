using System;

namespace CarsLogWorkig.Models
{
    public class VehicleComponent
    {
        private string _partName = string.Empty;
        public string PartName
        {
            get => _partName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назва компонента не може бути порожньою.");
                if (value.Trim().Length > 100)
                    throw new ArgumentException("Назва компонента не може перевищувати 100 символів.");
                _partName = value.Trim();
            }
        }

        public uint InstallationMileage { get; private set; }
        public bool IsExpired { get; private set; }
        public DateTime InstallationDate { get; private set; }

        public VehicleComponent(string partName, uint installationMileage,
                                bool isExpired, DateTime installationDate)
        {
            PartName = partName;
            InstallationMileage = installationMileage;
            IsExpired = isExpired;
            InstallationDate = installationDate;
        }

        public void MarkAsExpired()
        {
            IsExpired = true;
        }

        public override string ToString() =>
            $"[{_partName}] Встановлено: {InstallationDate:dd.MM.yyyy} | Пробіг: {InstallationMileage} км | Потребує заміни: {IsExpired}";
    }
}
