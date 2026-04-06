using System;
using System.Linq;

namespace CarsLogWorkig.Models
{
    public class User
    {
        private readonly Guid _id = Guid.NewGuid();
        public Guid Id => _id;

        private string _login = string.Empty;
        public string Login
        {
            get => _login;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Логін не може бути порожнім.");
                _login = value.Trim();
            }
        }

        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ім'я не може бути порожнім.");
                if (value.Trim().Length > 50)
                    throw new ArgumentException("Ім'я не може перевищувати 50 символів.");
                _firstName = value.Trim();
            }
        }

        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Прізвище не може бути порожнім.");
                if (value.Trim().Length > 50)
                    throw new ArgumentException("Прізвище не може перевищувати 50 символів.");
                _lastName = value.Trim();
            }
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Телефон не може бути порожнім.");
                _phone = value.Trim();
            }
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
                _passwordHash = value;
            }
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email не може бути порожнім.");
                if (value.Trim().Length > 100)
                    throw new ArgumentException("Email не може перевищувати 100 символів.");
                _email = value.Trim();
            }
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
        public string DateOfBirthFormatted => _dateOfBirth.ToString("dd.MM.yyyy");

        public UserRole Role { get; private set; }
        public UserSex Sex { get; private set; }
        public IsActiveUser IsActive { get; set; } = IsActiveUser.Ofline;
        public DateTime DateOfRegistration { get; private set; } = DateTime.UtcNow;
        public DateTime DateOfLastActivity { get; set; } = DateTime.UtcNow;
        public bool IsUserAgreedToRights { get; private set; }
        public bool CheckUserAgreement => IsUserAgreedToRights;
        public string FullName => $"{_firstName} {_lastName}".Trim();

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
            $"[{Role}] {FullName} | Email: {_email} | Active: {IsActive}";
    }

    public enum UserRole { Owner, Driver, Admin }

    public enum UserSex { Male, Female }

    public enum IsActiveUser
    {
        Online, Ofline, OutOfPlace, ComingSoon, DontDisturb, Invisible
    }
}
