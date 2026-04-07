using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasPurchaseDate
    {
        DateTime DateOfPurchaseTheCar { get; }
        string DateOfPurchaseTheCarFormatted { get; }
    }
}
