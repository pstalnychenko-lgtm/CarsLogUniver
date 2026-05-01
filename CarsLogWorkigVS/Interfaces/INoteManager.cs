using CarsLogWorkig.Models;
using System;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface INoteManager
    {
        List<Note> Notes { get; }
        void AddNote(Note note); 
        void DeleteNote(Guid noteId); 
    }
}
