using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PersonalVault.Application.Notes.Commands;
using PersonalVault.Application.Notes.Services;

namespace PersonalVault.Application.Notes.Commands.Handlers
{
    // Handles the UpdateNoteCommand.
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, bool>
    {
        private readonly INoteService _noteService;

        public UpdateNoteCommandHandler(INoteService noteService)
        {
            _noteService = noteService;
        }

        public async Task<bool> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            // Delegate the update operation to the NoteService.
            return await _noteService.UpdateNoteAsync(request.Id, request.UpdateDto);
        }
    }
}