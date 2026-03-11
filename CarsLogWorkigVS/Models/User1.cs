using System;

namespace CarsLogWorkig.Models
{
    public class User1
    {
        private string _login;
        
        private string _passwordHash;

        private string IdUser { get; set;}
        
        private string _email ;
        public string Email{ 
            get => _email;
            set => _email = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException() : value;
        }
        private UserRole _role { get; set; }


        public SubscriptionStatus SubscriptionStatus { get; set; }

        private DateTime DateOfBirth;

        private DateTime DateOfLastActivity;

        private DateTime DateOfRegistration;

        private UserSex UserSex;

        private List<Document> Documents { get; set; }

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

