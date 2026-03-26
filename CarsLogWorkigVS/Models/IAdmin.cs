using System;

namespace CarsLogWorkig.Models
{
    // Інтерфейс для адміністратора: управління користувачами та автомобілями в системі
    public interface IAdmin : IUser
    {
        
        string FirstName { get; }  // Переглянути ім'я адміністратора
        string LastName { get; }   // Переглянути прізвище адміністратора

        
        void DeactivateUser(User user);   // Деактивувати обліковий запис користувача
        void ActivateUser(User user);     // Активувати обліковий запис користувача
        void AssignRole(User user, UserRole role); // Призначити роль користувачу

        
        void AddVehicle(Vehicle vehicle);    // Додати автомобіль до системи
        void RemoveVehicle(Vehicle vehicle); // Видалити автомобіль з системи

        
        bool CanViewUserDetails(User user);    // Перевірити доступ до даних користувача
        bool CanEditVehicle(Vehicle vehicle);  // Перевірити доступ до редагування авто
    }
}
