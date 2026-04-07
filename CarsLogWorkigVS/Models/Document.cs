using System;

namespace CarsLogWorkig.Models
{
    public class Document
    {
        private readonly Guid _id = Guid.NewGuid();
        public Guid Id => _id;

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назва документа не може бути порожньою.");
                if (value.Trim().Length > 100)
                    throw new ArgumentException("Назва документа не може перевищувати 100 символів.");
                _title = value.Trim();
            }
        }

        public DateTime DateOfIssueDoc { get; private set; }
        public string DateOfIssueDocFormatted => DateOfIssueDoc.ToString("dd.MM.yyyy");
        public DocumentType DocumentType { get; private set; }

        private string _policyNumber = string.Empty;
        public string PolicyNumber
        {
            get => _policyNumber;
            private set
            {
                if (value != null && value.Trim().Length > 50)
                    throw new ArgumentException("Номер поліса не може перевищувати 50 символів.");
                _policyNumber = value?.Trim() ?? string.Empty;
            }
        }

        public Document(string title, DateTime dateOfIssueDoc, DocumentType documentType, string policyNumber = "")
        {
            Title = title;
            DateOfIssueDoc = dateOfIssueDoc;
            DocumentType = documentType;
            PolicyNumber = policyNumber;
        }

        public override string ToString() =>
            $"[{DocumentType}] {_title} | Видано: {DateOfIssueDocFormatted} | Номер: {_policyNumber}";
    }

    public enum DocumentType
    {
        VehicleRegistration,
        Insurance,
        TechnicalInspection,
        Other
    }
}
