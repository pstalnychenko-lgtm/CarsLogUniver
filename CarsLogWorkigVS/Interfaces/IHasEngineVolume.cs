namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasEngineVolume
    {
        uint EngineVolumeCc { get; }
        void ChangeEngineVolumeCc(uint newEngineVolumeCc);
    }
}
