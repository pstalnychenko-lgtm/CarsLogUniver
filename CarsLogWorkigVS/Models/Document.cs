using System;
using System.Data;

namespace CarsLogWorkig.Models
{
    public class Document
    {
        public Guid Id { get; init; } = Guid.NewGuid();// унікальний ідентифікатор документа

        public string Title { get; set; } // Назва документа 

        public DateTime DateOfIssueDoc { get; private set; }
        public string DateOfIssueDocFormatted => DateOfIssueDoc.ToString("dd.MM.yyyy");// Дата видачі документа

        public DocumentType DocumentType { get;private set; }// Тип документа (техпаспорт, страховий поліс, техогляд тощо)

        public string PolicyNumber { get; private set; }// Номер поліса (для страхового полісу)
    }

    public enum DocumentType
    {
        VehicleRegistration,  // Техпаспорт
        Insurance,            // Страховий поліс
        TechnicalInspection,  // Техогляд
        Other
    }
}
