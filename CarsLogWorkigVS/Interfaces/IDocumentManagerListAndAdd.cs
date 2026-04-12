using CarsLogWorkig.Models;
using System;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IDocumentManagerListAndAdd
    {
        List<Document> Documents { get; }
        void AddDocument(Document document);
    }
}
