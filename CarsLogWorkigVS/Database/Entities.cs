using SQLite;

namespace CarsLogWorkigVS.Database
{
    [Table("Users")]
    public class UserEntity
    {
        [PrimaryKey]
        public string Id { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public DateTime DateOfLastActivity { get; set; }
        public int Role { get; set; }
        public int Sex { get; set; }
        public int IsActive { get; set; }
        public string UserType { get; set; } = "User";
        public DateTime DateOfPurchaseTheCar { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public string LicenseIssuedBy { get; set; } = string.Empty;
        public DateTime LicenseExpiryDate { get; set; }
        public bool MedicalCertStatus { get; set; }
        public int BloodType { get; set; }
        public string LicenseCategoriesSerialized { get; set; } = "[]";
    }

    [Table("Vehicles")]
    public class VehicleEntity
    {
        [PrimaryKey]
        public string Id { get; set; } = string.Empty;
        public string PlateNumber { get; set; } = string.Empty;
        public string Vin { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string BodyType { get; set; } = string.Empty;
        public long EngineVolumeCc { get; set; }
        public int FuelType { get; set; }
        public decimal FuelTankCapacity { get; set; }
        public DateTime YearOfRelease { get; set; }
        public DateTime CarReleaseDate { get; set; }
        public long CurrentMileage { get; set; }
        public string GeneralNotes { get; set; } = string.Empty;
        [Indexed]
        public string OwnerId { get; set; } = string.Empty;
    }

    [Table("FuelEntries")]
    public class FuelEntryEntity
    {
        [PrimaryKey]
        public string Id { get; set; } = string.Empty;
        [Indexed]
        public string VehicleId { get; set; } = string.Empty;
        public string GasStationName { get; set; } = string.Empty;
        public string GasStationAddress { get; set; } = string.Empty;
        public int FuelType { get; set; }
        public decimal Liters { get; set; }
        public decimal PricePerLiter { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    [Table("ServiceRecords")]
    public class ServiceRecordEntity
    {
        [PrimaryKey]
        public string Id { get; set; } = string.Empty;
        [Indexed]
        public string VehicleId { get; set; } = string.Empty;
        public DateTime DateOfService { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public long MileageAtService { get; set; }
    }

    [Table("TripLogs")]
    public class TripLogEntity
    {
        [PrimaryKey]
        public string Id { get; set; } = string.Empty;
        [Indexed]
        public string VehicleId { get; set; } = string.Empty;
        public DateTime TripDate { get; set; }
        public string DeparturePoint { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public int Purpose { get; set; }
        public long StartMileage { get; set; }
        public long EndMileage { get; set; }
        public string Notes { get; set; } = string.Empty;
    }

    [Table("Expenses")]
    public class ExpenseEntity
    {
        [PrimaryKey]
        public string Id { get; set; } = string.Empty;
        [Indexed]
        public string VehicleId { get; set; } = string.Empty;
        public int Category { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime ExpenseDate { get; set; }
    }

    [Table("Documents")]
    public class DocumentEntity
    {
        [PrimaryKey]
        public string Id { get; set; } = string.Empty;
        [Indexed]
        public string VehicleId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime DateOfIssueDoc { get; set; }
        public int DocumentCategory { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
    }

    [Table("Notes")]
    public class NoteEntity
    {
        [PrimaryKey]
        public string Id { get; set; } = string.Empty;
        [Indexed]
        public string VehicleId { get; set; } = string.Empty;
        public string TitleNote { get; set; } = string.Empty;
        public string NoteContent { get; set; } = string.Empty;
        public int Category { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    [Table("VehicleComponents")]
    public class VehicleComponentEntity
    {
        [PrimaryKey, AutoIncrement]
        public int DbId { get; set; }
        [Indexed]
        public string VehicleId { get; set; } = string.Empty;
        public string PartName { get; set; } = string.Empty;
        public long InstallationMileage { get; set; }
        public bool IsExpired { get; set; }
        public DateTime InstallationDate { get; set; }
    }

    [Table("DriverVehicles")]
    public class DriverVehicleEntity
    {
        [PrimaryKey, AutoIncrement]
        public int DbId { get; set; }
        [Indexed]
        public string VehicleId { get; set; } = string.Empty;
        [Indexed]
        public string DriverId { get; set; } = string.Empty;
    }
}
