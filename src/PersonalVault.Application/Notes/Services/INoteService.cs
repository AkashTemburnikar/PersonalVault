using PersonalVault.Application.Notes.DTOs;

namespace PersonalVault.Application.Notes.Services;

public interface INoteService
{
    Task<Guid> CreateNoteAsync(NoteCreateDto dto);
    Task<NoteDto?> GetNoteByIdAsync(Guid id);
    Task<IEnumerable<NoteDto>> GetAllNotesAsync();
}