using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasYearOfRelease
    {
        DateTime YearOfRelease { get; }
        void ChangeYearOfRelease(DateTime newYear);
    }
}
