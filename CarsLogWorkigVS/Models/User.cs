using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class User
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        private string _login;
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

        private string _passwordHash;

        private string _email;

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

        private string _userFirstName { get; set; }
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
