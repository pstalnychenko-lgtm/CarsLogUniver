using System;

namespace CarsLogWorkig.Models
{
    public class Note // Клас для запису нотаток, пов'язаних з автомобілем
    {
        public Guid Id { get; init; } = Guid.NewGuid();// унікальний ідентифікатор нотатки

        private string _titleNote = string.Empty;
        public string TitleNote  // назва нотатки
        {
            get => _titleNote;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _titleNote = value;
            }
        }

        private string _noteContent = string.Empty;// зміст нотатки
        public string NoteContent
        {
            get => _noteContent;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _noteContent = value;
            }
        }

        public NoteCategory Category { get; private set; }// категорія нотатки

        public Note(string titleNote, string noteContent, NoteCategory category)// конструктор для створення запису про нотатку
        {
            TitleNote = titleNote;
            NoteContent = noteContent;
            Category = category;
        }
    }

    public enum NoteCategory
    {
        General,
        Fuel,
        Service,
        Finance,
        Reminder
    }
}
