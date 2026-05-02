using CarsLogWorkigVS.Interfaces;
using System;

namespace CarsLogWorkig.Models
{
    public class Admin : User, IUserRoleManager
    {
        public Admin(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Ім'я не може бути порожнім."); 
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Прізвище не може бути порожнім."); 

            ChangeFirstName(firstName); 
            ChangeLastName(lastName); 
            ChangeRole(UserRole.Admin); 
        }

        public void DeactivateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "Користувач не може бути порожнім."); 
            if (user.Role == UserRole.Admin)
                throw new InvalidOperationException("Адмін не може деактивувати іншого адміна."); 
            user.IsActive = IsActiveUser.Offline;
        }

        public void ActivateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "Користувач не може бути порожнім."); 
            user.IsActive = IsActiveUser.Online;
        }

        public void AssignRole(User user, UserRole role)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "Користувач не може бути порожнім."); 
            if (role == UserRole.Admin)
                throw new InvalidOperationException("Адмін не може призначати роль адміна. Це може зробити лише SuperAdmin."); 
            user.ChangeRole(role); 
        }

        public bool CanViewUserDetails(User user)
        {
            if (user == null) return false;
            return user.Role != UserRole.Admin;
        }

        public override string ToString() =>
            $"[Admin] {FullName} | Email: {Email} | Active: {IsActive}";
    }
}
