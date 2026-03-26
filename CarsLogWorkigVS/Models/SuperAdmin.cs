using System;
using CarsLogWorkig.Models;

namespace CarsLogWorkig.Models
{
    public class SuperAdmin : User, ISuperAdmin // клас супер-адміністратора
    {
        private string _nameSuperAdmin;
        public string NameSuperAdmin // властивість для зберігання імені супер-адміністратора
        {
            get => _nameSuperAdmin;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _nameSuperAdmin = value;
            }
        }

        private string _lastNameSuperAdmin;
        public string LastNameSuperAdmin // властивість для зберігання прізвища супер-адміністратора
        {
            get => _lastNameSuperAdmin;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _lastNameSuperAdmin = value;
            }
        }

        public SuperAdmin(string nameSuperAdmin, string lastNameSuperAdmin) // конструктор для створення супер-адміністратора
        {
            NameSuperAdmin = nameSuperAdmin;
            LastNameSuperAdmin = lastNameSuperAdmin;
            this.ChangeRole(UserRole.Admin); // SuperAdmin отримує найвищий наявний рівень ролі
        }

       
        
        
        
        
        // Управління 

        public void CreateAdmin(User user) // Призначити користувача адміністратором
        {
            if (user != null && user.Role != UserRole.Admin)
                user.ChangeRole(UserRole.Admin);
        }

        public void RemoveAdmin(User user) // Зняти права адміністратора — понизити до Driver
        {
            if (user != null && user.Role == UserRole.Admin)
                user.ChangeRole(UserRole.Driver);
        }

        public void DeactivateAdmin(User user) // Деактивувати адміністратора
        {
            if (user != null && user.Role == UserRole.Admin)
                user.IsActive = false;
        }

       
        
        
        
        
        // Управління автомобілями 


        public void AddVehicle(Vehicle vehicle){   }// Додати автомобіль
       

        public void RemoveVehicle(Vehicle vehicle) { } // Видалити автомобіль
        








        public bool IsSuperAdmin() // Перевірити чи є супер-адміном
        {
            return this is SuperAdmin;
        }
    }
}
