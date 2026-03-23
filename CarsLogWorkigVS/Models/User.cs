using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class User
    {
        public Guid Id { get; init; } = Guid.NewGuid(); /* унікальний ідентифікатор користувача ,який буде дійсний
                                                          і для інших сутностей, які будуть пов'язані з користувачем
                                                          (наприклад, нотатки, записи про сервісне обслуговування тощо) */

        private string _login;
        public string Login /* логін користувача, який буде використовуватися для входу в систему. 
                               Він повинен бути унікальним для кожного користувача.*/
        {
            get => _login;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _login = value;
            }
        }

        private string _passwordHash;

        private string _email;
        public string Email/* електронна пошта користувача, яка може використовуватися для відновлення пароля або 
                              для інших комунікацій. 
                              Вона також повинна бути унікальною для кожного користувача. */
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email не може бути порожнім.");
                else
                    _email = value;
            }
        }

        public UserRole Role { get; init; }/* роль користувача, яка визначає рівень доступу та дозволи в системі. 
                                    Це може бути корисно для реалізації різних рівнів доступу до функціональності програми 
                                    (наприклад, власник автомобіля, водій, адміністратор тощо).*/

        public UserSex Sex { get;private set; }/* стать користувача, яка може бути використана для персоналізації 
                                           інтерфейсу або для інших цілей. */
                                        

        public DateTime DateOfBirth { get; set; }
        public string DateOfBirthFormatted => DateOfBirth.ToString("dd.MM.yyyy");/* дата народження користувача, яка може використовуватися для 
                                                                                    персоналізації інтерфейсу або для інших цілей.*/

        public bool IsActive { get; set; } = true; /* статус активності користувача, який може використовуватися для визначення,
                                                     чи є обліковий запис активним чи ні. 
                                                    Це може бути корисно для блокування або видалення облікових записів, я
                                                    кі більше не використовуються.*/



        private string _userFirstName{ get; set; }/* ім'я користувача, яке може використовуватися для персоналізації
                                                     інтерфейсу або для інших цілей.*/
        public string UserFirstName 
        { 
            get => _userFirstName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _userFirstName = value;
            }
        }

        public DateTime DateOfRegistration { get; init; } = DateTime.UtcNow; /* Дата реєстрації користувача в системі, яка встановлюється 
                                                                                автоматично при створенні нового користувача.*/

        public DateTime DateOfLastActivity { get; set; } /* Дата останньої активності користувача в системі, яка
                                                          * може оновлюватися при кожному вході або виконанні певних дій. 
                                                          * Це може бути корисно для відстеження активності користувача та визначення 
                                                          * неактивних облікових записів.*/
    }

    public enum UserRole // Роль користувача, яка визначає рівень доступу та дозволи в системі.
    {
        Owner,
        Driver,
        Admin
    }

    public enum UserSex// Стать користувача, яка може бути використана для персоналізації інтерфейсу або для інших цілей.
    {
        Male,
        Female
    }
}