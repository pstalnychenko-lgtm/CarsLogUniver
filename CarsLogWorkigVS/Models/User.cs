using CarsLogWorkigVS.Interfaces;
using System;
using System.Linq;

namespace CarsLogWorkig.Models
{
    public class User : IWorkedWithLoginPhonePasswordETZ 
    {
        private readonly Guid _id = Guid.NewGuid(); 
        public Guid Id => _id;

        public string GetMaskedId()
        {
            var s = _id.ToString(); 
            return s.Substring(0, 8) + "-****-****-****-" + s.Substring(s.Length - 12); 
        }

        private string _login = string.Empty;
        public string Login => _login;

        public void ChangeLogin(string newLogin)
        {
            if (string.IsNullOrWhiteSpace(newLogin))
                throw new ArgumentException("Логін не може бути порожнім."); 
            if (_login == newLogin.Trim())
                throw new ArgumentException("Цей логін вже встановлено."); 
            _login = newLogin.Trim(); 
        }

        private string _address = string.Empty;
        public string Address => _address;

        public void ChangeAddress(string newAddress)
        {
            if (string.IsNullOrWhiteSpace(newAddress))
                throw new ArgumentException("Адреса не може бути порожньою."); 
            if (_address == newAddress.Trim())
                throw new ArgumentException("Ця адреса вже встановлена."); 
            _address = newAddress.Trim(); 
        }

        private string _firstName = string.Empty;
        public string FirstName => _firstName;

        private string _lastName = string.Empty;
        public string LastName => _lastName;

        public string FullName => $"{_firstName} {_lastName}".Trim(); 

        public void ChangeFirstName(string newFirstName)
        {
            if (string.IsNullOrWhiteSpace(newFirstName))
                throw new ArgumentException("Ім'я не може бути порожнім."); 
            if (newFirstName.Trim().Length > 50)
                throw new ArgumentException("Ім'я не може перевищувати 50 символів."); 
            if (_firstName == newFirstName.Trim())
                throw new ArgumentException("Це ім'я вже встановлено."); 
            _firstName = newFirstName.Trim(); 
        }

        public void ChangeLastName(string newLastName)
        {
            if (string.IsNullOrWhiteSpace(newLastName))
                throw new ArgumentException("Прізвище не може бути порожнім."); 
            if (newLastName.Trim().Length > 50)
                throw new ArgumentException("Прізвище не може перевищувати 50 символів."); 
            if (_lastName == newLastName.Trim())
                throw new ArgumentException("Це прізвище вже встановлено."); 
            _lastName = newLastName.Trim(); 
        }

        private string _phone = string.Empty;
        public string Phone => _phone;

        private void SetPhone(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Телефон не може бути порожнім."); 
            _phone = value.Trim(); 
        }

        public void ChangePhone(string newPhone)
        {
            if (string.IsNullOrWhiteSpace(newPhone))
                throw new ArgumentException("Телефон не може бути порожнім."); 
            if (_phone == newPhone.Trim())
                throw new ArgumentException("Цей телефон вже встановлено."); 
            SetPhone(newPhone); 
        }

        private string _password = string.Empty;
        public string Password => _password;

        private void SetPassword(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Пароль не може бути порожнім."); 
            if (value.Length < 8 || value.Length > 50)
                throw new ArgumentException("Пароль має містити щонайменше 8 символів і не більше 50 символів."); 
            if (!value.Any(char.IsUpper))
                throw new ArgumentException("Пароль має містити хоча б одну велику літеру."); 
            if (!value.Any(char.IsDigit))
                throw new ArgumentException("Пароль має містити хоча б одну цифру."); 
            if (_password == value)
                throw new ArgumentException("Новий пароль не може збігатися зі старим."); 
            if (value.All(char.IsLetter))
                throw new ArgumentException("Пароль має містити хоча б одне число і спеціальний символ."); 
            if (value.All(char.IsDigit))
                throw new ArgumentException("Пароль не може складатися лише з цифр."); 
            if (value.All(char.IsPunctuation))
                throw new ArgumentException("Пароль не може складатися лише зі спеціальних символів."); 
            if (value.Distinct().Count() < 4)
                throw new ArgumentException("Пароль занадто одноманітний. Використовуйте більше різних символів."); 

            var weakRoots = new[] { "password", "admin", "qwerty", "12345", "user", "root" };
            string lowerValue = value.ToLower(); 
            if (weakRoots.Any(root => lowerValue.Contains(root)))
                throw new ArgumentException("Пароль містить легко передбачувані слова або послідовності."); 

            _password = value;
        }

        public void ChangePassword(Guid requestingUserId, string newPassword)
        {
            if (_id != requestingUserId)
                throw new UnauthorizedAccessException("Відмовлено в доступі. Невірний ідентифікатор користувача."); 
            SetPassword(newPassword); 
        }

        private string _email = string.Empty;
        public string Email => _email;

        public void ChangeEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Email не може бути порожнім."); 
            if (newEmail.Trim().Length > 100)
                throw new ArgumentException("Email не може перевищувати 100 символів."); 
            if (!newEmail.Contains("@"))
                throw new ArgumentException("Некоректний формат Email."); 
            if (_email == newEmail.Trim())
                throw new ArgumentException("Цей Email вже встановлено."); 
            _email = newEmail.Trim(); 
        }

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth => _dateOfBirth;

        public void ChangeDateOfBirth(DateTime newDate)
        {
            if (newDate > DateTime.Now)
                throw new ArgumentException("Дата народження не може бути в майбутньому."); 
            if (_dateOfBirth == newDate)
                throw new ArgumentException("Ця дата народження вже встановлена."); 
            _dateOfBirth = newDate;
        }

        public string DateOfBirthFormatted => _dateOfBirth.ToString("dd.MM.yyyy"); 

        public UserRole Role { get; private set; }

        private UserSex _currentSex;
        public UserSex CurrentSex => _currentSex;

        public void ChangeSex(UserSex newSex)
        {
            if (_currentSex == newSex)
                throw new ArgumentException("Вказане значення вже встановлено."); 
            _currentSex = newSex;
        }

        public IsActiveUser IsActive { get; set; } = IsActiveUser.Offline;

        public void ChangeActivityStatus(IsActiveUser newStatus)
        {
            if (!Enum.IsDefined(typeof(IsActiveUser), newStatus))
                throw new ArgumentException("Недопустиме значення статусу."); 
            if (IsActive == newStatus)
                throw new ArgumentException("Цей статус вже встановлено."); 
            IsActive = newStatus;
        }

        public DateTime DateOfRegistration { get; private set; } = DateTime.UtcNow;
        public DateTime DateOfLastActivity { get; set; } = DateTime.UtcNow;

        public void UpdateDateOfLastActivity(DateTime newDate)
        {
            if (newDate > DateTime.UtcNow)
                throw new ArgumentException("Дата останньої активності не може бути в майбутньому."); 
            if (DateOfLastActivity == newDate)
                throw new ArgumentException("Ця дата вже встановлена."); 
            DateOfLastActivity = newDate;
        }

        private bool _isUserAgreedToRights;
        public bool CheckUserAgreement => _isUserAgreedToRights;

        public string UserRights => Role switch
        {
            UserRole.Owner => "Owner: Повна доступність до всіх функцій та налаштувань.",
            UserRole.Driver => "Driver: Доступ до функцій, пов'язаних з водінням, та журналів.",
            UserRole.Admin => "Admin: Доступ до управління користувачами та налаштувань системи.",
            _ => "Unknown role."
        };

        public User()
        {
            Role = UserRole.Driver;
        }

        protected User(string firstName, string lastName)
        {
            ChangeFirstName(firstName); 
            ChangeLastName(lastName); 
            Role = UserRole.Driver;
        }

        public void ChangeRole(UserRole role)
        {
            Role = role;
        }

        public override string ToString() =>
            $"[{Role}] {FullName} | Email: {_email} | Active: {IsActive}";
    }

    public enum UserRole { Owner, Driver, Admin }

    public enum UserSex { Male, Female }

    public enum IsActiveUser
    {
        Online, Offline, OutOfPlace, ComingSoon, DontDisturb, Invisible
    }
}
