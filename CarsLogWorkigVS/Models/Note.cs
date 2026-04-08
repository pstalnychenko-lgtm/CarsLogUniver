using CarsLogWorkigVS.Interfaces;
using System;

namespace CarsLogWorkig.Models
{
    public class Note : INoteManager
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

        private readonly List<Note> _notes = new List<Note>();

        public void AddNote(string titleNote, string noteContent, NoteCategory category)
        {
            try
            {
                var newNote = new Note(titleNote, noteContent, category);

                _notes.Add(newNote);
                Console.WriteLine($"[Успіх] Нотатку додано: {newNote.TitleNote}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"[Помилка додавання нотатки]: {ex.Message}");
            }
        }

        public void DeleteNote(Guid noteId)
        {
            var noteToRemove = _notes.FirstOrDefault(n => n.Id == noteId);
            if (noteToRemove != null)
            {
                _notes.Remove(noteToRemove);
                Console.WriteLine($"[Успіх] Видалено нотатку: {noteToRemove.TitleNote}");
            }
            else
            {
                Console.WriteLine("[Помилка] Нотатку з таким ID не знайдено.");
            }
        }

        public override string ToString() =>
            $"[{Category}] {_titleNote} | {CreatedAt:dd.MM.yyyy HH:mm}";
    }

    public enum NoteCategory { General, Fuel, Service, Finance, Reminder }
}
