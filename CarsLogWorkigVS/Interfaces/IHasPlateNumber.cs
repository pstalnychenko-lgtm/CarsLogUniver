namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasPlateNumber
    {
        string PlateNumber { get; }
        void ChangePlateNumber(string newPlateNumber);
    }
}
