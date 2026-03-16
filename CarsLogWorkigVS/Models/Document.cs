using System;

namespace CarsLogWorkig.Models
{
    public class Document
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string Title { get; set; }

        public DocumentType DocumentType { get;private set; }

        public string PolicyNumber { get;private set; }

        public DateTime IssueDate { get;private set; }
    }

    public enum DocumentType
    {
        VehicleRegistration,  // Техпаспорт
        Insurance,            // Страховий поліс
        TechnicalInspection,  // Техогляд
        Other
    }
}
