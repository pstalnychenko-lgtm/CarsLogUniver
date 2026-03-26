using System;

namespace CarsLogWorkig.Models
{
    public interface IUser
    {
        Guid Id { get; }
        string Email { get; set; }
        UserRole Role { get; }
        UserSex Sex { get; }
        bool IsActive { get; set; }
        DateTime DateOfBirth { get; set; }
        string DateOfBirthFormatted { get; }
        DateTime DateOfRegistration { get; }
        DateTime DateOfLastActivity { get; set; }
        string UserFirstName { get; }
    }
}
