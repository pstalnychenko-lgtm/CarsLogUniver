using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IAddsServiceRecord
    {
        void AddServiceRecord(Vehicle vehicle, ServiceRecord record);
    }
}
