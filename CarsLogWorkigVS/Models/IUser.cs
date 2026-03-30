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
        bool IsActive { get; set; }
        DateTime DateOfBirth { get; set; }
        string DateOfBirthFormatted { get; }
        DateTime DateOfRegistration { get; }
        DateTime DateOfLastActivity { get; set; }
        string UserFirstName { get; }
        
        bool CheckUserAgreement { get; }

    }
}
