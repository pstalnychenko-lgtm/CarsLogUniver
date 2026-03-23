using System;

namespace CarsLogWorkig.Models
{
    public class Admin : User
    {
        private string _firstName;
        public string FirstName 
        { 
            get;
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



        public AdminPosition Position { get;private set; }

        private string _contactInfo;
        public string ContactInfo 
        {
            get => _contactInfo;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _contactInfo = value;
            }
        }
        


    }

public enum AdminPosition
    {
        SuperAdmin,
        Manager,
        Editor,
        Viewer,

    }


}


