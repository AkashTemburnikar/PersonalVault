using MediatR;
using PersonalVault.Application.Notes.DTOs;
using PersonalVault.Application.Notes.Services;

namespace PersonalVault.Application.Notes.Queries.Handlers
{
    public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, NoteDto?>
    {
        private readonly INoteService _noteService;
        
        public GetNoteByIdQueryHandler(INoteService noteService)
        {
            _noteService = noteService;
        }
        
        public async Task<NoteDto?> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
        {
            return await _noteService.GetNoteByIdAsync(request.Id);
        }
    }
}