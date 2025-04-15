using PersonalVault.Domain.Entities;

namespace PersonalVault.Application.Common.Interfaces;

public interface INoteRepository
{
    Task<Guid> AddNoteAsync(Note note);
    Task<Note?> GetNoteByIdAsync(Guid id);
    Task<IEnumerable<Note>> GetAllNotesAsync();
}