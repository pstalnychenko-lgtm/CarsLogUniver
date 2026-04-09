namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasBrand
    {
        string Brand { get; }
        void ChangeBrand(string newBrand);
    }
}
