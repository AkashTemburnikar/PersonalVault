using PersonalVault.Application.Notes.DTOs;

namespace PersonalVault.Application.Notes.Services;

public interface INoteService
{
    Task<Guid> CreateNoteAsync(NoteCreateDto dto);
    Task<NoteDto?> GetNoteByIdAsync(Guid id);
    Task<IEnumerable<NoteDto>> GetAllNotesAsync();
    Task<bool> UpdateNoteAsync(Guid id, NoteUpdateDto dto);
    Task<bool> DeleteNoteAsync(Guid id);
}