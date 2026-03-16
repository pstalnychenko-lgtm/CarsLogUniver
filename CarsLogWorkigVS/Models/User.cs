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

        public UserRole Role { get; set; }

        public UserSex Sex { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfRegistration { get; init; } = DateTime.UtcNow;

        public DateTime DateOfLastActivity { get; set; } 
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