using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class User
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        private string _login = string.Empty;
        public string Login
        {
            get => _login;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _login = value;
            }
        }

        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _firstName = value;
            }
        }

        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _lastName = value;
            }
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _phone = value;
            }
        }

        // Зберігаємо пароль 
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
                if (!value.All(char.IsLetterOrDigit))
                    throw new ArgumentException("Пароль не повинен містити спеціальних символів.");
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
                _email = value;
            }
        }

        public UserRole Role { get; private set; }
        public UserSex Sex { get; private set; }

        public DateTime DateOfBirth { get; set; }
        public string DateOfBirthFormatted => DateOfBirth.ToString("dd.MM.yyyy");

        public bool IsActive { get; set; } = true;

        public DateTime DateOfRegistration { get; init; } = DateTime.UtcNow;
        public DateTime DateOfLastActivity { get; set; }

        public bool IsUserAgreedToRights { get; private set; }
        public bool CheckUserAgreement => IsUserAgreedToRights;

        public User()
        {
            this.Role = UserRole.Driver;
        }

        public void ChangeRole(UserRole role)
        {
            this.Role = role;
        }

        public string UserRights => Role switch
        {
            UserRole.Owner => "Owner: Full access to all features and settings.",
            UserRole.Driver => "Driver: Access to driving-related features and logs.",
            UserRole.Admin => "Admin: Access to user management and system settings.",
            _ => "Unknown role."
        };

        // Зручний метод для відображення повного імені
        public string FullName => $"{FirstName} {LastName}".Trim();

        public override string ToString() =>
            $"[{Role}] {FullName} | Email: {Email} | Active: {IsActive}";
    }

    public enum UserRole
    {
        Owner,
        Driver,
        Admin
    }

    public enum UserSex
    {
        Male,
        Female
    }
}
