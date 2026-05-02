using CarsLogWorkigVS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

        internal Note(string titleNote, string noteContent, NoteCategory category, DateTime createdAt)
        {
            TitleNote = titleNote;
            NoteContent = noteContent;
            Category = category;
            CreatedAt = createdAt;
        }

        public override string ToString() =>
            $"[{Category}] {_titleNote} | {CreatedAt:dd.MM.yyyy HH:mm}";
    }

    public class NoteManager : INoteRepository
    {
        private readonly List<Note> _notes = new List<Note>(); 

        public List<Note> Notes => _notes;

        public void AddNote(Note note)
        {
            if (note == null)
                throw new ArgumentNullException(nameof(note), "Нотатка не може бути порожньою."); 
            if (_notes.Any(n => n.Id == note.Id))
                throw new InvalidOperationException("Ця нотатка вже додана."); 
            _notes.Add(note); 
        }

        public void DeleteNote(Guid noteId)
        {
            var note = _notes.FirstOrDefault(n => n.Id == noteId); 
            if (note == null)
                throw new ArgumentException("Нотатку з таким ID не знайдено."); 
            _notes.Remove(note); 
        }
    }

    public enum NoteCategory { General, Fuel, Service, Finance, Reminder }
}
