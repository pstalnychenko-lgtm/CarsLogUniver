using System;

namespace CarsLogWorkig.Models
{
    public class Admin : User, IAdmin // клас адмін
    {
        private string _firstName;
        public string FirstName // властивість для зберігання імені адміністратора
        {
            get => _firstName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _firstName = value;
            }
        }

        private string _lastName;
        public string LastName // властивість для зберігання прізвища адміністратора
        {
            get => _lastName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _lastName = value;
            }
        }

        //Управління користувачами

        public void DeactivateUser(User user) // Деактивувати обліковий запис користувача
        {
            if (user != null && user.Role != UserRole.Admin)
                user.IsActive = false;
        }

        public void ActivateUser(User user) // Активувати обліковий запис користувача
        {
            if (user != null)
                user.IsActive = true;
        }

        public void AssignRole(User user, UserRole role) // Призначити роль — але не вище Admin
        {
            if (user != null && role != UserRole.Admin)
                user.ChangeRole(role);
        }

        //Управління автомобілями

        public void AddVehicle(Vehicle vehicle) { }// Додати автомобіль до системи
        public void RemoveVehicle(Vehicle vehicle) { } // Видалити автомобіль з системи
        

        



        public bool CanViewUserDetails(User user) // Адмін бачить дані всіх, крім інших адмінів
        {
            if (user == null) return false;
            return user.Role != UserRole.Admin;
        }

        public bool CanEditVehicle(Vehicle vehicle) // Адмін може редагувати будь-який автомобіль
        {
            return vehicle != null;
        }
    }
}
