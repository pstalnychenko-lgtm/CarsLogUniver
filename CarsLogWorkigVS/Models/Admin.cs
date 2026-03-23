using System;

namespace CarsLogWorkig.Models
{
    public class Admin : User
    {
        private string _firstName; 
        public string FirstName // властивість для зберігання імені адміністратора
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

        private string _lastName; // властивість для зберігання прізвища адміністратора
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



        public AdminPosition Position { get;private set; } /* властивість для зберігання посади адміністратора*/                                                          
                                                            

        private string _contactInfo;
        public string ContactInfo // властивість для зберігання контактної інформації адміністратора
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

public enum AdminPosition // Перелік посад адміністратора
    {
        SuperAdmin,
        Manager,
        Editor,
        Viewer,

    }


}


