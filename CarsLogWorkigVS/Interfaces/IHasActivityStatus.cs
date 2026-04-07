using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasActivityStatus
    {
        IsActiveUser IsActive { get; set; }
    }
}
