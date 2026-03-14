using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class User
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string Login { get; private set; }

        private string _passwordHash;

        private string _email;
        public string Email
        {
            get => _email;
            set => _email = string.IsNullOrWhiteSpace(value)
                ? throw new ArgumentException("Email не може бути порожнім.")
                : value;
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
