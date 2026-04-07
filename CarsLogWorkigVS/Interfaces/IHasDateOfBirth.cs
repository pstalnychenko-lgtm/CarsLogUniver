using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasDateOfBirth
    {
        DateTime DateOfBirth { get; set; }
        string DateOfBirthFormatted { get; }
        void ChangeDateOfBirth(DateTime newDate);
    }
}
