using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PersonalVault.Application.Notes.DTOs;
using PersonalVault.Application.Notes.Queries;
using PersonalVault.Application.Notes.Services;

namespace PersonalVault.Application.Notes.Queries.Handlers
{
    // Handles the GetAllNotesQuery.
    public class GetAllNotesQueryHandler : IRequestHandler<GetAllNotesQuery, IEnumerable<NoteDto>>
    {
        private readonly INoteService _noteService;

        public GetAllNotesQueryHandler(INoteService noteService)
        {
            _noteService = noteService;
        }

        public async Task<IEnumerable<NoteDto>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
        {
            // Delegate to the NoteService to retrieve all notes.
            return await _noteService.GetAllNotesAsync();
        }
    }
}