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
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _login = value;
            }
        }



        // Зберігаємо пароль у вигляді хешу для безпеки
        private string _passwordHash = string.Empty;

        public string PasswordHash
        {
            get => _passwordHash;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Пароль не може бути порожнім.");
                }

                if (value.Length < 8)
                {
                    throw new ArgumentException("Пароль має містити щонайменше 8 символів.");
                }

                if (!value.Any(char.IsUpper))
                {
                    throw new ArgumentException("Пароль має містити хоча б одну велику літеру.");
                }

                if (!value.Any(char.IsDigit))
                {
                    throw new ArgumentException("Пароль має містити хоча б одну цифру.");
                }

               
                if (!value.All(char.IsLetterOrDigit))
                {
                    throw new ArgumentException("Пароль не повинен містити спеціальних символів.");
                }

                _passwordHash = value;
            }
        }

        private string _email = string.Empty;

        public User()
        {
            this.Role = UserRole.Driver;
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email не може бути порожнім.");
                else
                    _email = value;
            }
        }

        public UserRole Role { get; private set; }

        public UserSex Sex { get; private set; }

        public DateTime DateOfBirth { get; set; }
        public string DateOfBirthFormatted => DateOfBirth.ToString("dd.MM.yyyy");

        public bool IsActive { get; set; } = true;

        private string _userFirstName { get; set; } = string.Empty;
        public string UserFirstName
        {
            get => _userFirstName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _userFirstName = value;
            }
        }

        public DateTime DateOfRegistration { get; init; } = DateTime.UtcNow;

        public DateTime DateOfLastActivity { get; set; }

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

        public bool IsUserAgreedToRights { get; private set; }

        // Implemented as a property to satisfy IUser interface
        public bool CheckUserAgreement => IsUserAgreedToRights;
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
