using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IWorkedWithFuelEntry
    {
        void SetGasStation(string gasStationName, string gasStationAddress); 
        void SetFuelType(FuelsType fuelType); 
        void ChangeGasStationName(string newName); 
        void ChangeGasStationAddress(string newAddress); 
    }
}
