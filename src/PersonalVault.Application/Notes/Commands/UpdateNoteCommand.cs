using MediatR;
using System;
using PersonalVault.Application.Notes.DTOs;

namespace PersonalVault.Application.Notes.Commands
{
    // Command to update a note.
    public class UpdateNoteCommand : IRequest<bool>
    {
        public Guid Id { get; }
        public NoteUpdateDto UpdateDto { get; }

        public UpdateNoteCommand(Guid id, NoteUpdateDto updateDto)
        {
            Id = id;
            UpdateDto = updateDto;
        }
    }
}