namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasColor
    {
        string Color { get; }
        void ChangeColor(string newColor);
    }
}
