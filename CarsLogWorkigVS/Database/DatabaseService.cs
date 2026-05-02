using CarsLogWorkig.Models;
using SQLite;
using System.Text.Json;
using System.Threading;

namespace CarsLogWorkigVS.Database
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _db;
        private readonly SemaphoreSlim _initLock = new SemaphoreSlim(1, 1);

        private static string DbPath =>
            Path.Combine(FileSystem.AppDataDirectory, "carslog.db3");

        private async Task InitAsync()
        {
            if (_db != null) return;

            await _initLock.WaitAsync();
            try
            {
                if (_db != null) return;

                _db = new SQLiteAsyncConnection(DbPath,
                    SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

                await _db.CreateTableAsync<UserEntity>();
                await _db.CreateTableAsync<VehicleEntity>();
                await _db.CreateTableAsync<FuelEntryEntity>();
                await _db.CreateTableAsync<ServiceRecordEntity>();
                await _db.CreateTableAsync<TripLogEntity>();
                await _db.CreateTableAsync<ExpenseEntity>();
                await _db.CreateTableAsync<DocumentEntity>();
                await _db.CreateTableAsync<NoteEntity>();
                await _db.CreateTableAsync<VehicleComponentEntity>();
                await _db.CreateTableAsync<DriverVehicleEntity>();
            }
            finally
            {
                _initLock.Release();
            }
        }

        private async Task<SQLiteAsyncConnection> GetDb()
        {
            await InitAsync();
            return _db!;
        }

        public async Task SaveUserAsync(User user)
        {
            var db = await GetDb();
            var entity = MapUserToEntity(user);
            var existing = await db.FindAsync<UserEntity>(user.Id.ToString());
            if (existing == null)
                await db.InsertAsync(entity);
            else
                await db.UpdateAsync(entity);
        }

        public async Task SaveUserWithPasswordAsync(User user, string plainPassword)
        {
            var db = await GetDb();
            var entity = MapUserToEntity(user);
            entity.Password = PasswordHasher.Hash(plainPassword);
            var existing = await db.FindAsync<UserEntity>(user.Id.ToString());
            if (existing == null)
                await db.InsertAsync(entity);
            else
                await db.UpdateAsync(entity);
        }

        public async Task<bool> VerifyPasswordAsync(string login, string plainPassword)
        {
            var db = await GetDb();
            var entity = await db.Table<UserEntity>()
                .Where(u => u.Login == login)
                .FirstOrDefaultAsync();
            if (entity == null) return false;
            if (string.IsNullOrEmpty(entity.Password)) return false;
            return PasswordHasher.Verify(plainPassword, entity.Password);
        }

        public async Task<User?> GetUserByLoginAsync(string login)
        {
            var db = await GetDb();
            var entity = await db.Table<UserEntity>()
                .Where(u => u.Login == login)
                .FirstOrDefaultAsync();
            return entity == null ? null : MapEntityToUser(entity);
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            var db = await GetDb();
            var entity = await db.FindAsync<UserEntity>(id);
            return entity == null ? null : MapEntityToUser(entity);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var db = await GetDb();
            var entities = await db.Table<UserEntity>().ToListAsync();
            return entities.Select(MapEntityToUser).ToList();
        }

        public async Task DeleteUserAsync(string id)
        {
            var db = await GetDb();
            await db.DeleteAsync<UserEntity>(id);
        }

        public async Task SaveVehicleAsync(Vehicle vehicle)
        {
            var db = await GetDb();
            var entity = new VehicleEntity
            {
                Id = vehicle.Id.ToString(),
                PlateNumber = vehicle.PlateNumber,
                Vin = vehicle.Vin,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Color = vehicle.Color,
                BodyType = vehicle.BodyType,
                EngineVolumeCc = vehicle.EngineVolumeCc,
                FuelType = (int)vehicle.FuelType,
                FuelTankCapacity = vehicle.FuelTankCapacity,
                YearOfRelease = vehicle.YearOfRelease,
                CarReleaseDate = vehicle.CarReleaseDate,
                CurrentMileage = vehicle.CurrentMileage,
                GeneralNotes = vehicle.GeneralNotes,
                OwnerId = vehicle.Owner.Id.ToString()
            };

            var existing = await db.FindAsync<VehicleEntity>(entity.Id);
            if (existing == null)
                await db.InsertAsync(entity);
            else
                await db.UpdateAsync(entity);
        }

        public async Task<List<VehicleEntity>> GetVehiclesForOwnerAsync(string ownerId)
        {
            var db = await GetDb();
            return await db.Table<VehicleEntity>()
                .Where(v => v.OwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<VehicleEntity?> GetVehicleByIdAsync(string vehicleId)
        {
            var db = await GetDb();
            return await db.FindAsync<VehicleEntity>(vehicleId);
        }

        public async Task DeleteVehicleAsync(string vehicleId)
        {
            var db = await GetDb();
            await db.DeleteAsync<VehicleEntity>(vehicleId);
            await db.Table<FuelEntryEntity>().DeleteAsync(x => x.VehicleId == vehicleId);
            await db.Table<ServiceRecordEntity>().DeleteAsync(x => x.VehicleId == vehicleId);
            await db.Table<TripLogEntity>().DeleteAsync(x => x.VehicleId == vehicleId);
            await db.Table<ExpenseEntity>().DeleteAsync(x => x.VehicleId == vehicleId);
            await db.Table<DocumentEntity>().DeleteAsync(x => x.VehicleId == vehicleId);
            await db.Table<NoteEntity>().DeleteAsync(x => x.VehicleId == vehicleId);
            await db.Table<VehicleComponentEntity>().DeleteAsync(x => x.VehicleId == vehicleId);
            await db.Table<DriverVehicleEntity>().DeleteAsync(x => x.VehicleId == vehicleId);
        }

        public async Task SaveFuelEntryAsync(string vehicleId, FuelEntry entry)
        {
            var db = await GetDb();
            var entity = new FuelEntryEntity
            {
                Id = entry.Id.ToString(),
                VehicleId = vehicleId,
                GasStationName = entry.GasStationName,
                GasStationAddress = entry.GasStationAddress,
                FuelType = (int)entry.FuelType,
                Liters = entry.Liters,
                PricePerLiter = entry.PricePerLiter,
                TotalCost = entry.TotalCost,
                CreatedAt = DateTime.UtcNow
            };

            var existing = await db.FindAsync<FuelEntryEntity>(entity.Id);
            if (existing == null)
                await db.InsertAsync(entity);
            else
                await db.UpdateAsync(entity);
        }

        public async Task<List<FuelEntryEntity>> GetFuelEntriesAsync(string vehicleId)
        {
            var db = await GetDb();
            return await db.Table<FuelEntryEntity>()
                .Where(f => f.VehicleId == vehicleId)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task DeleteFuelEntryAsync(string id)
        {
            var db = await GetDb();
            await db.DeleteAsync<FuelEntryEntity>(id);
        }

        public async Task SaveServiceRecordAsync(string vehicleId, ServiceRecord record)
        {
            var db = await GetDb();
            var entity = new ServiceRecordEntity
            {
                Id = record.Id.ToString(),
                VehicleId = vehicleId,
                DateOfService = record.DateOfService,
                Description = record.Description,
                Cost = record.Cost,
                MileageAtService = record.MileageAtService
            };

            var existing = await db.FindAsync<ServiceRecordEntity>(entity.Id);
            if (existing == null)
                await db.InsertAsync(entity);
            else
                await db.UpdateAsync(entity);
        }

        public async Task<List<ServiceRecordEntity>> GetServiceRecordsAsync(string vehicleId)
        {
            var db = await GetDb();
            return await db.Table<ServiceRecordEntity>()
                .Where(s => s.VehicleId == vehicleId)
                .OrderByDescending(s => s.DateOfService)
                .ToListAsync();
        }

        public async Task DeleteServiceRecordAsync(string id)
        {
            var db = await GetDb();
            await db.DeleteAsync<ServiceRecordEntity>(id);
        }

        public async Task SaveTripLogAsync(string vehicleId, TripLog trip)
        {
            var db = await GetDb();
            var entity = new TripLogEntity
            {
                Id = trip.Id.ToString(),
                VehicleId = vehicleId,
                TripDate = trip.TripDate,
                DeparturePoint = trip.DeparturePoint,
                Destination = trip.Destination,
                Purpose = (int)trip.Purpose,
                StartMileage = trip.StartMileage,
                EndMileage = trip.EndMileage,
                Notes = trip.Notes
            };

            var existing = await db.FindAsync<TripLogEntity>(entity.Id);
            if (existing == null)
                await db.InsertAsync(entity);
            else
                await db.UpdateAsync(entity);
        }

        public async Task<List<TripLogEntity>> GetTripLogsAsync(string vehicleId)
        {
            var db = await GetDb();
            return await db.Table<TripLogEntity>()
                .Where(t => t.VehicleId == vehicleId)
                .OrderByDescending(t => t.TripDate)
                .ToListAsync();
        }

        public async Task DeleteTripLogAsync(string id)
        {
            var db = await GetDb();
            await db.DeleteAsync<TripLogEntity>(id);
        }

        public async Task SaveExpenseAsync(Expense expense)
        {
            var db = await GetDb();
            var entity = new ExpenseEntity
            {
                Id = expense.Id.ToString(),
                VehicleId = expense.VehicleId.ToString(),
                Category = (int)expense.Category,
                Amount = expense.Amount,
                Description = expense.Description,
                ExpenseDate = expense.ExpenseDate
            };

            var existing = await db.FindAsync<ExpenseEntity>(entity.Id);
            if (existing == null)
                await db.InsertAsync(entity);
            else
                await db.UpdateAsync(entity);
        }

        public async Task<List<ExpenseEntity>> GetExpensesAsync(string vehicleId)
        {
            var db = await GetDb();
            return await db.Table<ExpenseEntity>()
                .Where(e => e.VehicleId == vehicleId)
                .OrderByDescending(e => e.ExpenseDate)
                .ToListAsync();
        }

        public async Task DeleteExpenseAsync(string id)
        {
            var db = await GetDb();
            await db.DeleteAsync<ExpenseEntity>(id);
        }

        public async Task SaveDocumentAsync(string vehicleId, Document document)
        {
            var db = await GetDb();
            var entity = new DocumentEntity
            {
                Id = document.Id.ToString(),
                VehicleId = vehicleId,
                Title = document.Title,
                DateOfIssueDoc = document.DateOfIssueDoc,
                DocumentCategory = (int)document.DocumentCategory,
                PolicyNumber = document.PolicyNumber
            };

            var existing = await db.FindAsync<DocumentEntity>(entity.Id);
            if (existing == null)
                await db.InsertAsync(entity);
            else
                await db.UpdateAsync(entity);
        }

        public async Task<List<DocumentEntity>> GetDocumentsAsync(string vehicleId)
        {
            var db = await GetDb();
            return await db.Table<DocumentEntity>()
                .Where(d => d.VehicleId == vehicleId)
                .OrderByDescending(d => d.DateOfIssueDoc)
                .ToListAsync();
        }

        public async Task DeleteDocumentAsync(string id)
        {
            var db = await GetDb();
            await db.DeleteAsync<DocumentEntity>(id);
        }

        public async Task SaveNoteAsync(string vehicleId, Note note)
        {
            var db = await GetDb();
            var entity = new NoteEntity
            {
                Id = note.Id.ToString(),
                VehicleId = vehicleId,
                TitleNote = note.TitleNote,
                NoteContent = note.NoteContent,
                Category = (int)note.Category,
                CreatedAt = note.CreatedAt
            };

            var existing = await db.FindAsync<NoteEntity>(entity.Id);
            if (existing == null)
                await db.InsertAsync(entity);
            else
                await db.UpdateAsync(entity);
        }

        public async Task<List<NoteEntity>> GetNotesAsync(string vehicleId)
        {
            var db = await GetDb();
            return await db.Table<NoteEntity>()
                .Where(n => n.VehicleId == vehicleId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task DeleteNoteAsync(string id)
        {
            var db = await GetDb();
            await db.DeleteAsync<NoteEntity>(id);
        }

        public async Task SaveComponentAsync(string vehicleId, VehicleComponent component)
        {
            var db = await GetDb();
            var entity = new VehicleComponentEntity
            {
                VehicleId = vehicleId,
                PartName = component.PartName,
                InstallationMileage = component.InstallationMileage,
                IsExpired = component.IsExpired,
                InstallationDate = component.InstallationDate
            };
            await db.InsertAsync(entity);
        }

        public async Task<List<VehicleComponentEntity>> GetComponentsAsync(string vehicleId)
        {
            var db = await GetDb();
            return await db.Table<VehicleComponentEntity>()
                .Where(c => c.VehicleId == vehicleId)
                .ToListAsync();
        }

        public async Task UpdateComponentAsync(VehicleComponentEntity entity)
        {
            var db = await GetDb();
            await db.UpdateAsync(entity);
        }

        public async Task DeleteComponentAsync(int dbId)
        {
            var db = await GetDb();
            await db.DeleteAsync<VehicleComponentEntity>(dbId);
        }

        public async Task LinkDriverToVehicleAsync(string vehicleId, string driverId)
        {
            var db = await GetDb();
            var existing = await db.Table<DriverVehicleEntity>()
                .Where(d => d.VehicleId == vehicleId && d.DriverId == driverId)
                .FirstOrDefaultAsync();
            if (existing == null)
                await db.InsertAsync(new DriverVehicleEntity { VehicleId = vehicleId, DriverId = driverId });
        }

        public async Task UnlinkDriverFromVehicleAsync(string vehicleId, string driverId)
        {
            var db = await GetDb();
            await db.Table<DriverVehicleEntity>()
                .DeleteAsync(d => d.VehicleId == vehicleId && d.DriverId == driverId);
        }

        public async Task<List<string>> GetDriverIdsForVehicleAsync(string vehicleId)
        {
            var db = await GetDb();
            var links = await db.Table<DriverVehicleEntity>()
                .Where(d => d.VehicleId == vehicleId)
                .ToListAsync();
            return links.Select(l => l.DriverId).ToList();
        }

        private static UserEntity MapUserToEntity(User user)
        {
            var entity = new UserEntity
            {
                Id = user.Id.ToString(),
                Login = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Email = user.Email,
                Address = user.Address,
                Password = user.Password,
                DateOfBirth = user.DateOfBirth,
                DateOfRegistration = user.DateOfRegistration,
                DateOfLastActivity = user.DateOfLastActivity,
                Role = (int)user.Role,
                Sex = (int)user.CurrentSex,
                IsActive = (int)user.IsActive
            };

            if (user is Owner owner)
            {
                entity.UserType = "Owner";
                entity.DateOfPurchaseTheCar = owner.DateOfPurchaseTheCar;
            }
            else if (user is Driver driver)
            {
                entity.UserType = "Driver";
                entity.LicenseNumber = driver.LicenseNumber;
                entity.LicenseIssuedBy = driver.LicenseIssuedBy;
                entity.LicenseExpiryDate = driver.LicenseExpiryDate;
                entity.MedicalCertStatus = driver.MedicalCertStatus;
                entity.BloodType = (int)driver.BloodType;
                entity.LicenseCategoriesSerialized = JsonSerializer.Serialize(driver.LicenseCategories);
            }
            else if (user is SuperAdmin)
            {
                entity.UserType = "SuperAdmin";
            }
            else if (user is Admin)
            {
                entity.UserType = "Admin";
            }

            return entity;
        }

        private static User MapEntityToUser(UserEntity e)
        {
            User user;

            switch (e.UserType)
            {
                case "Owner":
                    var owner = new Owner(
                        e.FirstName.Length > 0 ? e.FirstName : "Невідомо",
                        e.LastName.Length > 0 ? e.LastName : "Невідомо",
                        e.Phone.Length > 0 ? e.Phone : "+380000000000",
                        e.Address.Length > 0 ? e.Address : "Не вказано",
                        e.DateOfPurchaseTheCar == default ? DateTime.Now.AddYears(-1) : e.DateOfPurchaseTheCar
                    );
                    user = owner;
                    break;

                case "Driver":
                    var driver = new Driver(
                        e.FirstName.Length > 0 ? e.FirstName : "Невідомо",
                        e.LastName.Length > 0 ? e.LastName : "Невідомо",
                        e.Phone.Length > 0 ? e.Phone : "+380000000000",
                        e.LicenseNumber.Length > 0 ? e.LicenseNumber : "DRV-0000",
                        e.LicenseIssuedBy.Length > 0 ? e.LicenseIssuedBy : "ТСЦ",
                        e.LicenseExpiryDate == default ? DateTime.Now.AddYears(3) : e.LicenseExpiryDate,
                        e.MedicalCertStatus,
                        (BloodType)e.BloodType
                    );

                    if (!string.IsNullOrEmpty(e.LicenseCategoriesSerialized))
                    {
                        try
                        {
                            var categories = JsonSerializer.Deserialize<List<LicenseCategory>>(e.LicenseCategoriesSerialized);
                            if (categories != null)
                            {
                                foreach (var cat in categories)
                                {
                                    driver.AddLicenseCategory(cat);
                                }
                            }
                        }
                        catch { throw; }
                    }
                    user = driver;
                    break;

                case "SuperAdmin":
                    user = new SuperAdmin(
                        e.FirstName.Length > 0 ? e.FirstName : "Super",
                        e.LastName.Length > 0 ? e.LastName : "Admin"
                    );
                    break;

                default:
                    user = new User();
                    if (e.FirstName.Length > 0) user.ChangeFirstName(e.FirstName);
                    if (e.LastName.Length > 0) user.ChangeLastName(e.LastName);
                    break;
            }

            try { if (e.Login.Length > 0) user.ChangeLogin(e.Login); } catch { throw; }
            try { if (e.Email.Length > 0) user.ChangeEmail(e.Email); } catch { throw; }
            try { if (e.Address.Length > 0 && user is not Owner) user.ChangeAddress(e.Address); } catch { throw; }
            user.IsActive = (IsActiveUser)e.IsActive;

            return user;
        }
    }
}