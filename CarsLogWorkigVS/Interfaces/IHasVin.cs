namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasVin
    {
        string Vin { get; }
        void ChangeVin(string newVin);
    }
}
