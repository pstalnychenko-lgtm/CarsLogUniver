using System;

namespace CarsLogWorkig.Models
{
    // Базовий інтерфейс для всіх користувачів системи
    public interface IUser
    {
        Guid Id { get; }              // Переглянути свій ID
        string Email { get; set; }    // Переглянути/змінити email
        UserRole Role { get; }        // Переглянути свою роль
        UserSex Sex { get; }          // Переглянути стать
        bool IsActive { get; set; }   // Переглянути статус активності
        DateTime DateOfBirth { get; set; }          // Переглянути дату народження
        string DateOfBirthFormatted { get; }        // Відформатована дата народження
        DateTime DateOfRegistration { get; }        // Переглянути дату реєстрації
        DateTime DateOfLastActivity { get; set; }   // Переглянути/оновити дату останньої активності
        string UserFirstName { get; }               // Переглянути ім'я
    }
}
