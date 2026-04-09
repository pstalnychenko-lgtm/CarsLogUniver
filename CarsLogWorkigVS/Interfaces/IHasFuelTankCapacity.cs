namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasFuelTankCapacity
    {
        decimal FuelTankCapacity { get; }
        void ChangeFuelTankCapacity(decimal newCapacity);
    }
}
