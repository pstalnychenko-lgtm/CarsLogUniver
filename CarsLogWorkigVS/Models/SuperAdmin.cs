using System;

namespace CarsLogWorkig.Models
{
    public class SuperAdmin : User, ISuperAdmin
    {
        public SuperAdmin(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Ім'я не може бути порожнім.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Прізвище не може бути порожнім.");

            FirstName = firstName;
            LastName = lastName;
            this.ChangeRole(UserRole.Admin);
        }

        public void CreateAdmin(User user)
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
            if (user.Role != UserRole.Admin)
                user.ChangeRole(UserRole.Admin);
        }

        public void RemoveAdmin(User user)
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
            if (user.Role == UserRole.Admin)
                user.ChangeRole(UserRole.Driver);
        }

        public void DeactivateAdmin(User user)
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
            if (user.Role == UserRole.Admin)
                user.IsActive = IsActiveUser.Ofline;
        }

        public void DeactivateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("Користувач не може бути порожнім.");
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
            user.ChangeRole(role);
        }

        public bool IsSuperAdmin()
        {
            return this is SuperAdmin;
        }

        public override string ToString() =>
            $"[SuperAdmin] {FullName} | Email: {Email} | Active: {IsActive}";
    }
}
