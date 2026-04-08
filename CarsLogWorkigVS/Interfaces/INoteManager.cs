using CarsLogWorkig.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkigVS.Interfaces
{
    public interface INoteManager
    {
        
        void AddNote(string titleNote, string noteContent, NoteCategory category);

        void DeleteNote(Guid noteId);
    }
}
