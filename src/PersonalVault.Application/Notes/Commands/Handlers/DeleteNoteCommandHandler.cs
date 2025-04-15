using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PersonalVault.Application.Notes.Commands;
using PersonalVault.Application.Notes.Services;

namespace PersonalVault.Application.Notes.Commands.Handlers
{
    // Handles the DeleteNoteCommand.
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, bool>
    {
        private readonly INoteService _noteService;

        public DeleteNoteCommandHandler(INoteService noteService)
        {
            _noteService = noteService;
        }

        public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            // Delegate the delete operation to the NoteService.
            return await _noteService.DeleteNoteAsync(request.Id);
        }
    }
}