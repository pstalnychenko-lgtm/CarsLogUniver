using System;

namespace CarsLogWorkig.Models
{
   
    public class Admin : User, IAdmin
    {
        public void DeactivateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
            if (user.Role == UserRole.Admin)
                throw new InvalidOperationException("Адмін не може деактивувати іншого адміна.");
            user.IsActive = IsActiveUser.Ofline;
        }

        public void ActivateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
            user.IsActive = IsActiveUser.Online;
        }

        public void AssignRole(User user, UserRole role)
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
            if (role == UserRole.Admin)
                throw new InvalidOperationException("Адмін не може призначати роль адміна. Це може зробити лише SuperAdmin.");
            user.ChangeRole(role);
        }

        public bool CanViewUserDetails(User user)
        {
            if (user == null) return false;
            return user.Role != UserRole.Admin;
        }
    }
}
