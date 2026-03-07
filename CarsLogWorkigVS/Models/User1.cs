using System;
using System.Collections.Generic;
using System.Text;


namespace CarsLogWorkig.Models
{
    public class User1
    {
        private string _login;
        
        private string _passwordHash;

        public Guid Id { get; private set; }
        
        public string Login
        {
            get => _login;
            set => _login = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException() : value;
        }
        
        public string PasswordHash
        {
            get => _passwordHash;
            set => _passwordHash = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException() : value;
        }
        
        public string SubscriptionStatus { get; set; }

        
    }
}