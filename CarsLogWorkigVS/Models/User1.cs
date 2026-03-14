using System;

namespace CarsLogWorkig.Models
{
    public class User1
    {
        private string _login;
        
        private string _passwordHash;

        private string _idUser { get; init;}
        
        private string _email ;
        private string Email{ 
            get => _email;
            set => _email = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException() : value;
        }
        private UserRole _role { get; set; }


        public SubscriptionStatus SubscriptionStatus { get; set; }

        private DateTime _dateOfBirth;

        private DateTime _dateOfLastActivity;

        private DateTime _dateOfRegistration;

        private UserSex _userSex;

        private List<Document> _documents { get; set; }

    }

    public enum SubscriptionStatus
    {
        Active,
        Inactive,
        Expired
    }

    public enum UserRole
    {
        Owner,
        Driver,
        Admin
    }

    public enum UserSex 
    { 
    Male,
    Female
    }
}

