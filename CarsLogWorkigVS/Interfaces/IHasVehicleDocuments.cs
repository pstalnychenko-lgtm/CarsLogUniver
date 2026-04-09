using CarsLogWorkig.Models;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasVehicleDocuments
    {
        List<Document> Documents { get; }
    }
}
