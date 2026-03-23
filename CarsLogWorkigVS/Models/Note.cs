using System;

namespace CarsLogWorkig.Models
{
    public class Note // Клас для запису нотаток, пов'язаних з автомобілем
    {
        public Guid Id { get; init; } = Guid.NewGuid();// унікальний ідентифікатор нотатки

        private string _title;
        public string Title
        {
            get => _title;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _title = value;
            }
        }

        private string _content;// зміст нотатки
        public string Content
        {
            get => _content;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _content = value;
            }
        }

        public NoteCategory Category { get; private set; }// категорія нотатки

        public Note(string title, string content, NoteCategory category)// конструктор для створення запису про нотатку
        {
            Title = title;
            Content = content;
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
