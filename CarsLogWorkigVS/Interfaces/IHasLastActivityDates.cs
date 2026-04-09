using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasLastActivityDates
    {
        void UpdateDateOfLastActivity(DateTime newDate);
    }
}
