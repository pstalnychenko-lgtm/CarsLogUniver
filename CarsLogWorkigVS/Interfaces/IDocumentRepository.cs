using CarsLogWorkig.Models;
using System;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IDocumentRepository
    {
        List<Document> Documents { get; }
        void AddDocument(Document document); 
        void DeleteDocument(Guid documentId); 
    }
}
