namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasMileage
    {
        uint CurrentMileage { get; set; }
        void ChangeCurrentMileage(uint newMileage);
        uint GetTotalDistance();
    }
}
