using CarsLogWorkig.Models;
using System;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface INoteRepository
    {
        List<Note> Notes { get; }
        void AddNote(Note note); 
        void DeleteNote(Guid noteId); 
    }
}
