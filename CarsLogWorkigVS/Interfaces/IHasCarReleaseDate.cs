using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasCarReleaseDate
    {
        DateTime CarReleaseDate { get; }
        void ChangeCarReleaseDate(DateTime newDate);
    }
}
