using System;

namespace CarsLogWorkig.Models
{
    public class Note
    {
        private readonly Guid _id = Guid.NewGuid();
        public Guid Id => _id;

        private string _titleNote = string.Empty;
        public string TitleNote
        {
            get => _titleNote;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назва нотатки не може бути порожньою.");
                if (value.Trim().Length > 100)
                    throw new ArgumentException("Назва нотатки не може перевищувати 100 символів.");
                _titleNote = value.Trim();
            }
        }

        private string _noteContent = string.Empty;
        public string NoteContent
        {
            get => _noteContent;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Зміст нотатки не може бути порожнім.");
                if (value.Trim().Length > 1000)
                    throw new ArgumentException("Зміст нотатки не може перевищувати 1000 символів.");
                _noteContent = value.Trim();
            }
        }

        public NoteCategory Category { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public Note(string titleNote, string noteContent, NoteCategory category)
        {
            TitleNote = titleNote;
            NoteContent = noteContent;
            Category = category;
        }

        public override string ToString() =>
            $"[{Category}] {_titleNote} | {CreatedAt:dd.MM.yyyy HH:mm}";
    }

    public enum NoteCategory { General, Fuel, Service, Finance, Reminder }
}
