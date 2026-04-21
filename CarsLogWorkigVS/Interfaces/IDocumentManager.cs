using CarsLogWorkig.Models;
using System;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IDocumentManager
    {
        List<Document> Documents { get; }
        void AddDocument(Document document);
        void DeleteDocument(Guid documentId);
    }
}
