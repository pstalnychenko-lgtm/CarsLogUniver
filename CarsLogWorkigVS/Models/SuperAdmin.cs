using CarsLogWorkigVS.Interfaces;
using System;

namespace CarsLogWorkig.Models
{
    public class SuperAdmin : Admin,
        IManagesAdminsRemove,
        IManagesAdminsDeactivate,
        IManagesAdminCreate,
        IViewsAsUser
    {
        private UserViewSession? _currentViewSession;

        public bool IsInViewAsMode => _currentViewSession != null;
        public UserViewSession? CurrentViewSession => _currentViewSession;

        public SuperAdmin(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Ім'я не може бути порожнім.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Прізвище не може бути порожнім.");

            ChangeFirstName(firstName);
            ChangeLastName(lastName);
            this.ChangeRole(UserRole.Admin);
        }

        public UserViewSession StartViewAs(User targetUser)
        {
            if (targetUser == null)
                throw new ArgumentNullException(nameof(targetUser));
            if (targetUser.Id == this.Id)
                throw new InvalidOperationException("SuperAdmin не може переглядати систему від власного імені.");
            if (IsInViewAsMode)
                throw new InvalidOperationException("Сесія перегляду вже активна. Завершіть поточну перед початком нової.");

            _currentViewSession = new UserViewSession(targetUser);
            return _currentViewSession;
        }

        public void EndViewAs(UserViewSession session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));
            if (_currentViewSession == null || _currentViewSession.SessionId != session.SessionId)
                throw new InvalidOperationException("Вказана сесія не є активною.");

            _currentViewSession = null;
        }

        public void CreateAdmin(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "Користувач не може бути порожнім.");
            if (user.Role != UserRole.Admin)
                user.ChangeRole(UserRole.Admin);
        }

        public void RemoveAdmin(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "Користувач не може бути порожнім.");
            if (user.Role == UserRole.Admin)
                user.ChangeRole(UserRole.Driver);
        }

        public void DeactivateAdmin(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "Користувач не може бути порожнім.");
            if (user.Role == UserRole.Admin)
                user.IsActive = IsActiveUser.Offline;
        }

        public override string ToString() =>
            $"[SuperAdmin] {FullName} | Email: {Email} | Active: {IsActive}";
    }
}
