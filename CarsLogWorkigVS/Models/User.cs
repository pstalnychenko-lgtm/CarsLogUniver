using CarsLogWorkigVS.Interfaces;
using System;
using System.Linq;

namespace CarsLogWorkig.Models
{
    public class User :
        IHasId,
        IHasFullName,
        IHasPhone,
        IHasEmail,
        IHasLogin,
        IHasPasswordHash,
        IHasRole,
        IHasActivityStatus,
        IHasDateOfBirth,
        IHasRegistrationDates,
        IHasSex,
        IHasUserAgreement
        
    {

        private readonly Guid _id = Guid.NewGuid();

        public Guid Id => _id;
        public string Login { get; private set; }

        public void ChangeLogin(string newLogin)
        {
            if (string.IsNullOrWhiteSpace(newLogin))
            {
                throw new ArgumentException("Логін не може бути порожнім.");
            }

            if (Login == newLogin)
            {
                throw new ArgumentException("Цей логін вже встановлено.");
            }

            Login = newLogin;
        }
        public string Address { get; set; }

            public void ChangeAddress(string newAddress)
            {
                if (string.IsNullOrWhiteSpace(newAddress))
                {
                    throw new ArgumentException("Адреса не може бути порожньою.");
                }

                if (Address == newAddress)
                {
                    throw new ArgumentException("Ця адреса вже встановлена.");
                }

                Address = newAddress;
            }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();

        public void ChangeFirstName(string newFirstName)
        {
            if (string.IsNullOrWhiteSpace(newFirstName))
            {
                throw new ArgumentException("Ім'я не може бути порожнім.");
            }

            if (FirstName == newFirstName)
            {
                throw new ArgumentException("Це ім'я вже встановлено.");
            }

            FirstName = newFirstName;
        }

        public void ChangePatronymic(string newPatronymic)
        {
            if (string.IsNullOrWhiteSpace(newPatronymic))
            {
                throw new ArgumentException("По батькові не може бути порожнім.");
            }

            if (LastName == newPatronymic)
            {
                throw new ArgumentException("Це по батькові вже встановлено.");
            }

            LastName = newPatronymic;
        }

        public string Phone { get; set; }

        public void ChangePhone(string newPhone)
        {
            if (string.IsNullOrWhiteSpace(newPhone))
            {
                throw new ArgumentException("Телефон не може бути порожнім.");
            }

            if (Phone == newPhone)
            {
                throw new ArgumentException("Цей телефон вже встановлено.");
            }

            Phone = newPhone;
        }

        

        private string _passwordHash = string.Empty;

        public string PasswordHash
        {
            get => _passwordHash;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Пароль не може бути порожнім.");

                if (value.Length < 8)
                    throw new ArgumentException("Пароль має містити щонайменше 8 символів.");

                if (!value.Any(char.IsUpper))
                    throw new ArgumentException("Пароль має містити хоча б одну велику літеру.");

                if (!value.Any(char.IsDigit))
                    throw new ArgumentException("Пароль має містити хоча б одну цифру.");

                if (_passwordHash == value)
                    throw new ArgumentException("Новий пароль не може збігатися зі старим.");

                _passwordHash = value;
            }
        }

        public void ChangePassword(Guid requestingUserId, string newPassword)
        {
            if (Id != requestingUserId)
            {
                throw new UnauthorizedAccessException("Відмовлено в доступі. Невірний ідентифікатор користувача.");
            }

            PasswordHash = newPassword;
        }

        public bool VerePassword { get; set; }

        public string Email { get; private set; }

        public void ChangeEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
            {
                throw new ArgumentException("Email не може бути порожнім.");
            }

            if (Email == newEmail)
            {
                throw new ArgumentException("Цей Email вже встановлено.");
            }

            if (!newEmail.Contains("@"))
            {
                throw new ArgumentException("Некоректний формат Email.");
            }

            Email = newEmail;
        }

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Дата народження не може бути в майбутньому.");
                _dateOfBirth = value;
            }
        }
        public void ChangeDateOfBirth(DateTime newDate)
        {
            if (DateOfBirth == newDate)
                throw new ArgumentException("Ця дата народження вже встановлена.");

            DateOfBirth = newDate;
        }
        public string DateOfBirthFormatted => _dateOfBirth.ToString("dd.MM.yyyy");

        public UserRole Role { get; private set; }
        public UserSex CurrentSex { get; private set; }

        public void ChangeSex(UserSex newSex)
          {
                if (CurrentSex == newSex)
                {
                    throw new ArgumentException("Вказане значення вже встановлено.");
                }

                CurrentSex = newSex;
          }

        public IsActiveUser IsActive { get; set; } = IsActiveUser.Offline;

        public void ChangeActivityStatus(IsActiveUser newStatus)
        {
            if (!Enum.IsDefined(typeof(IsActiveUser), newStatus))
            {
                throw new ArgumentException("Недопустиме значення статусу.");
            }

            if (IsActive == newStatus)
            {
                throw new ArgumentException("Цей статус вже встановлено.");
            }

            IsActive = newStatus;
        }
        public DateTime DateOfRegistration { get; private set; } = DateTime.UtcNow;
        public DateTime DateOfLastActivity { get; set; } = DateTime.UtcNow;
        public bool IsUserAgreedToRights { get; private set; }
        public bool CheckUserAgreement => IsUserAgreedToRights;

        public string UserRights => Role switch
        {
            UserRole.Owner => "Owner: Full access to all features and settings.",
            UserRole.Driver => "Driver: Access to driving-related features and logs.",
            UserRole.Admin => "Admin: Access to user management and system settings.",
            _ => "Unknown role."
        };

        public User()
        {
            Role = UserRole.Driver;
        }

        public void ChangeRole(UserRole role)
        {
            Role = role;
        }

        public override string ToString() =>
            $"[{Role}] {FullName} | Email: {Email} | Active: {IsActive}";
    }

    public enum UserRole { Owner, Driver, Admin }

    public enum UserSex { Male, Female }

    public enum IsActiveUser
    {
        Online, Offline, OutOfPlace, ComingSoon, DontDisturb, Invisible
    }
}
