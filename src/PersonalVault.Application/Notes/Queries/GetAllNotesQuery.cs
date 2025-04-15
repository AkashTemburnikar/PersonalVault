using MediatR;
using PersonalVault.Application.Notes.DTOs;
using System.Collections.Generic;

namespace PersonalVault.Application.Notes.Queries
{
    // Query to get all notes.
    public class GetAllNotesQuery : IRequest<IEnumerable<NoteDto>>
    {
        // No additional parameters needed.
    }
}