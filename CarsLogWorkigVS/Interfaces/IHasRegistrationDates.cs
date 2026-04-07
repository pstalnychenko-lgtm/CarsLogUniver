using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasRegistrationDates
    {
        DateTime DateOfRegistration { get; }
        DateTime DateOfLastActivity { get; set; }
    }
}
