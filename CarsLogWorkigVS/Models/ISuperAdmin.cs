using System;

namespace CarsLogWorkig.Models
{
    public interface ISuperAdmin : IUser
    {
        string NameSuperAdmin { get; }
        string LastNameSuperAdmin { get; }


        // Методи для управління адміністраторами
        void CreateAdmin(User user);
        void RemoveAdmin(User user);
        void DeactivateAdmin(User user);


        // Методи для управління користувачами
        void DeactivateUser(User user);
        void ActivateUser(User user);
        void AssignRole(User user, UserRole role);


        // Метод для перевірки, чи є користувач супер-адміністратором
        bool IsSuperAdmin();
    }
}
