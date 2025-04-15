using MediatR;
using System;
using PersonalVault.Application.Notes.DTOs;

namespace PersonalVault.Application.Notes.Commands
{
    // This command carries the data required to create a new note.
    public class CreateNoteCommand : IRequest<Guid>
    {
        public NoteCreateDto NoteDto { get; }
        
        public CreateNoteCommand(NoteCreateDto noteDto)
        {
            NoteDto = noteDto;
        }
    }
}