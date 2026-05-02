using CarsLogWorkig.Models;
using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IUserCredentials
    {
        void ChangeLogin(string newLogin); 
        void ChangePhone(string newPhone); 
        void ChangePassword(Guid requestingUserId, string newPassword); 
        void ChangeEmail(string newEmail); 
        void ChangeFirstName(string newFirstName); 
        void ChangeLastName(string newLastName); 
        void ChangeRole(UserRole role); 
        void ChangeSex(UserSex newSex); 
        IsActiveUser IsActive { get; set; }
        void ChangeActivityStatus(IsActiveUser newStatus); 
        void ChangeDateOfBirth(DateTime newDate); 
        void UpdateDateOfLastActivity(DateTime newDate); 
    }
}
