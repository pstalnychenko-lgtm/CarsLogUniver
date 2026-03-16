using System;

namespace CarsLogWorkig.Models
{
    public class Note
    {
        public Guid Id { get; init; } = Guid.NewGuid();

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

        private string _content;
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

        public NoteCategory Category { get; private set; }

        public Note(string title, string content, NoteCategory category)
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
