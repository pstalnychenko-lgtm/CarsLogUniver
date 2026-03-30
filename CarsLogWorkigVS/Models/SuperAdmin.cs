using System;

namespace CarsLogWorkig.Models
{
    public class SuperAdmin : User, ISuperAdmin
    {
        private string _nameSuperAdmin = string.Empty;
        public string NameSuperAdmin
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

        private string _lastNameSuperAdmin = string.Empty;
        public string LastNameSuperAdmin
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

        public SuperAdmin(string nameSuperAdmin, string lastNameSuperAdmin)
        {
            NameSuperAdmin = nameSuperAdmin;
            LastNameSuperAdmin = lastNameSuperAdmin;
            this.ChangeRole(UserRole.Admin);
        }


        // Методи для управління адміністраторами
        public void CreateAdmin(User user)
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
            if (user.Role != UserRole.Admin)
                user.ChangeRole(UserRole.Admin);
        }


        // Методи для управління користувачами
        public void RemoveAdmin(User user)
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
            if (user.Role == UserRole.Admin)
                user.ChangeRole(UserRole.Driver);
        }


        // Методи для управління користувачами
        public void DeactivateAdmin(User user)
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
            if (user.Role == UserRole.Admin)
                user.IsActive = false;
        }


        // Методи для управління користувачами
        public void DeactivateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
            user.IsActive = false;
        }


        // Методи для управління користувачами
        public void ActivateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
            user.IsActive = true;
        }

        public void AssignRole(User user, UserRole role) // Метод для призначення ролі користувачу
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
            user.ChangeRole(role);
        }

        public bool IsSuperAdmin() //Метод для перевірки, чи є користувач супер-адміністратором
        {
            return this is SuperAdmin;
        }
    }
}
