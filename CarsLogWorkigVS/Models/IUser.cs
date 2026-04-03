using System;

namespace CarsLogWorkig.Models
{
    public interface IUser
    {
        Guid Id { get; }
        string UserRights { get; }
        string Email { get; set; }
        UserRole Role { get; }
        string PasswordHash { get; }
        UserSex Sex { get; }
        IsActiveUser IsActive { get; set; }
        DateTime DateOfBirth { get; set; }
        string DateOfBirthFormatted { get; }
        DateTime DateOfRegistration { get; }
        DateTime DateOfLastActivity { get; set; }

        // Спільні поля для всіх користувачів
        string FirstName { get; set; }
        string LastName { get; set; }
        string Phone { get; set; }
        string FullName { get; }

        bool CheckUserAgreement { get; }
    }
}
