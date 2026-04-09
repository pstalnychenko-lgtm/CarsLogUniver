namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasVehicleModel
    {
        string Model { get; }
        void ChangeModel(string newModel);
    }
}
