using CarsLogWorkig.Models;
using System;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IDocumentManagerDelete
    {
        void DeleteDocument(Guid documentId);
    }
}
