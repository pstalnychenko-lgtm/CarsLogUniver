using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasLastActivityDates
    {
        DateTime DateOfLastActivity { get; set; }
        void UpdateDateOfLastActivity(DateTime newDate);
    }
}
