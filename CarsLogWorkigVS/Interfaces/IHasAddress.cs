namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasAddress
    {
        string Address { get; }
        void ChangeAddress(string newAddress);
    }
}
