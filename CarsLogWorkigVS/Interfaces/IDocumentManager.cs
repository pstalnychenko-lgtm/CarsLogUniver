using CarsLogWorkig.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IDocumentManager
    {
        
        void AddDocument(string title, DateTime dateOfIssueDoc, DocumentType documentType, string policyNumber = "");

        void DeleteDocument(Guid documentId);
    }
}
