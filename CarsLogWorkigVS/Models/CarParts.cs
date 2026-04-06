using System;

namespace CarsLogWorkig.Models
{
    public class CarParts
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        private string _customPartName = string.Empty;
        public string CustomPartName
        {
            get => _customPartName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назва запчастини не може бути порожньою.");
                if (value.Trim().Length > 100)
                    throw new ArgumentException("Назва запчастини не може перевищувати 100 символів.");
                _customPartName = value.Trim();
            }
        }

        public bool IsReplaced { get; private set; }
        public bool IsBeInNormalCondition { get; private set; }

        private string _notesComponents = string.Empty;
        public string NotesComponents
        {
            get => _notesComponents;
            private set
            {
                if (value != null && value.Trim().Length > 1000)
                    throw new ArgumentException("Нотатки не можуть перевищувати 1000 символів.");
                _notesComponents = value?.Trim() ?? string.Empty;
            }
        }

        public CarParts(string customPartName, bool isReplaced,
                                  bool isBeInNormalCondition, string notesComponents)
        {
            CustomPartName = customPartName;
            IsReplaced = isReplaced;
            IsBeInNormalCondition = isBeInNormalCondition;
            NotesComponents = notesComponents;
        }

        public void MarkAsReplaced()
        {
            IsReplaced = true;
            IsBeInNormalCondition = true;
        }

        public override string ToString() =>
            $"[{_customPartName}] Замінено: {IsReplaced} | В нормі: {IsBeInNormalCondition}";
    }
}
