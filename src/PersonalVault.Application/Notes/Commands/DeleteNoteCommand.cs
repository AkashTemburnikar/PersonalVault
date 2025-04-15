using MediatR;
using System;

namespace PersonalVault.Application.Notes.Commands
{
    // Command to delete a note.
    public class DeleteNoteCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public DeleteNoteCommand(Guid id)
        {
            Id = id;
        }
    }
}