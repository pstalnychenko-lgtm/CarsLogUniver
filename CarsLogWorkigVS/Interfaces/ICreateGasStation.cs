using System;
using System.Collections.Generic;
using System.Text;


namespace CarsLogWorkigVS.Interfaces
{
    public interface ICreateGasStation
    {
        void CreateGasStationName();
        void CreateGasStationAddress();
        void UpdateGasStationName();
        void UpdateGasStationAddress();
        void CreateGasStation(string gasStationName, string gasStationAddress);
    }
}
