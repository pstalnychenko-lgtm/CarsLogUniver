using System;

namespace CarsLogWorkig.Models
{
    public class Admin : User, IAdmin
    {
        private string _firstName;
        public string FirstName
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
        public string LastName
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

        public void DeactivateUser(User user) // деактивація користувача
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
            if (user.Role == UserRole.Admin)
                throw new InvalidOperationException("Адмін не може деактивувати іншого адміна.");
            user.IsActive = false;
        }

        public void ActivateUser(User user) // активація користувача
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
            user.IsActive = true;
        }

        public void AssignRole(User user, UserRole role) // призначення ролі користувачу
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
            if (role == UserRole.Admin)
                throw new InvalidOperationException("Адмін не може призначати роль адміна. Це може зробити лише SuperAdmin.");
            user.ChangeRole(role);
        }

        public bool CanViewUserDetails(User user) // перевірка, чи може адмін переглядати деталі користувача
        {
            if (user == null) return false;
            return user.Role != UserRole.Admin;
        }
    }
}
