using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasLastActiityDates
    {
        DateTime DateOfLastActivity { get; set; }
        void UpdateDateOfLastActivity(DateTime newDate);
    }
}
