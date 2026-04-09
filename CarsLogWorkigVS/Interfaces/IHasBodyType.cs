namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasBodyType
    {
        string BodyType { get; }
        void ChangeBodyType(string newBodyType);
    }
}
