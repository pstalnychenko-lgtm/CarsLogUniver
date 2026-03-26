using System;

namespace CarsLogWorkig.Models
{
    public interface IAdmin : IUser
    {
        string FirstName { get; }
        string LastName { get; }

        void DeactivateUser(User user); // Метод для деактивації користувача
        void ActivateUser(User user); // Метод для активації користувача
        void AssignRole(User user, UserRole role); // Метод для назначененя ролі користувачу

        bool CanViewUserDetails(User user); // Метод для перевірки, чи може адміністратор переглядати деталі користувача
    }
}
