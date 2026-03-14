using System;

namespace CarsLogWorkig.Models
{
    public class Note
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string Title { get; set; }

        public string Content { get; set; }

        public NoteCategory Category { get; set; }
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
