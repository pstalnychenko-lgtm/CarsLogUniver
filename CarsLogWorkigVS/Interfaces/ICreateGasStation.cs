namespace CarsLogWorkigVS.Interfaces
{
    public interface ICreateGasStation
    {
        string GasStationName { get; }
        string GasStationAddress { get; }

        void SetGasStation(string gasStationName, string gasStationAddress);
        void ChangeGasStationName(string newName);
        void ChangeGasStationAddress(string newAddress);
    }
}
