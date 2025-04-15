using MediatR;
using PersonalVault.Application.Notes.Services;

namespace PersonalVault.Application.Notes.Commands.Handlers
{
    // The handler receives the command and processes it.
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly INoteService _noteService;
        
        public CreateNoteCommandHandler(INoteService noteService)
        {
            _noteService = noteService;
        }
        
        public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            // Delegate to the existing NoteService to create the note.
            return await _noteService.CreateNoteAsync(request.NoteDto);
        }
    }
}