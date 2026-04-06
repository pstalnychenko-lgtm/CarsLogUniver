using System;

namespace CarsLogWorkig.Models
{
    public class LicenseCategory
    {
        private readonly Guid _id = Guid.NewGuid();
        public Guid Id => _id;

        public DateTime DateOfIssue { get; private set; }
        public string DateOfIssueFormatted => DateOfIssue.ToString("dd.MM.yyyy");

        public DateTime ExpirationDate { get; private set; }
        public string ExpirationDateFormatted => ExpirationDate.ToString("dd.MM.yyyy");

        private string _cityOfIssue = string.Empty;
        public string CityOfIssue
        {
            get => _cityOfIssue;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Місто видачі не може бути порожнім.");
                _cityOfIssue = value.Trim();
            }
        }

        private string _serialNumber = string.Empty;
        public string SerialNumber
        {
            get => _serialNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Серійний номер не може бути порожнім.");
                _serialNumber = value.Trim();
            }
        }

        private string _trafficPoliceCenter = string.Empty;
        public string TrafficPoliceCenter
        {
            get => _trafficPoliceCenter;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Центр ДАІ не може бути порожнім.");
                _trafficPoliceCenter = value.Trim();
            }
        }

        public CategoryName CategoryName { get; private set; }

        public LicenseCategory(DateTime dateOfIssue, DateTime expirationDate,
                                string cityOfIssue, string serialNumber,
                                string trafficPoliceCenter, CategoryName categoryName)
        {
            if (expirationDate <= dateOfIssue)
                throw new ArgumentException("Дата закінчення має бути пізніше дати видачі.");

            DateOfIssue = dateOfIssue;
            ExpirationDate = expirationDate;
            CityOfIssue = cityOfIssue;
            SerialNumber = serialNumber;
            TrafficPoliceCenter = trafficPoliceCenter;
            CategoryName = categoryName;
        }

        public void VerifyCanDrive(CategoryName vehicleCategory)
        {
            if (CategoryName != vehicleCategory)
                throw new InvalidOperationException($"У вас немає права керувати транспортом категорії {vehicleCategory}.");
        }

        public bool IsValid() => ExpirationDate > DateTime.Now;

        public override string ToString() =>
            $"[Категорія {CategoryName}] Серія: {_serialNumber} | Дійсна до: {ExpirationDateFormatted} | {_trafficPoliceCenter}";
    }

    public enum CategoryName
    {
        A = 1, A1 = 2, B = 3, B1 = 4,
        C = 5, C1 = 6, D = 7, D1 = 8,
        CE = 9, C1E = 10, BE = 11, T1 = 12, T2 = 13
    }
}
