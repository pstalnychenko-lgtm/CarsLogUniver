using System;

namespace CarsLogWorkig.Models
{
    // Інтерфейс для супер-адміністратора: повний контроль над системою
    public interface ISuperAdmin : IUser
    {
        //Особисті дані
        string NameSuperAdmin { get; }      // Переглянути ім'я супер-адміна
        string LastNameSuperAdmin { get; }  // Переглянути прізвище супер-адміна





        //Управління
        void CreateAdmin(User user);        // Призначити користувача адміністратором
        void RemoveAdmin(User user);        // Зняти права адміністратора
        void DeactivateAdmin(User user);    // Деактивувати адміністратора

        

        //Управління автомобілями 
        void AddVehicle(Vehicle vehicle);    // Додати автомобіль
        void RemoveVehicle(Vehicle vehicle); // Видалити автомобіль

        



        bool IsSuperAdmin();  // Перевірити чи є супер-адміном
    }
}
