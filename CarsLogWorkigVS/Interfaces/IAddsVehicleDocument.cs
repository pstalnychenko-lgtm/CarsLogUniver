using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IAddsVehicleDocument
    {
        void AddDocumentToVehicle(Vehicle vehicle, Document document);
    }
}
