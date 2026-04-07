using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IAddsVehicleNote
    {
        void AddNoteToVehicle(Vehicle vehicle, Note note);
    }
}
