using PersonalVault.Application.Common.Interfaces;
using PersonalVault.Application.Notes.DTOs;
using PersonalVault.Domain.Entities;

namespace PersonalVault.Application.Notes.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;
    
    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    
    public async Task<Guid> CreateNoteAsync(NoteCreateDto dto)
    {
        var note = new Note
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Content = dto.Content,
            CreatedAt = DateTime.UtcNow
        };
        
        return await _noteRepository.AddNoteAsync(note);
    }
    
    public async Task<NoteDto?> GetNoteByIdAsync(Guid id)
    {
        var note = await _noteRepository.GetNoteByIdAsync(id);
        if (note is null)
            return null;
        
        return new NoteDto
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CreatedAt = note.CreatedAt
        };
    }
    
    // New method to get all notes
    public async Task<IEnumerable<NoteDto>> GetAllNotesAsync()
    {
        var notes = await _noteRepository.GetAllNotesAsync();
        return notes.Select(note => new NoteDto
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CreatedAt = note.CreatedAt
        });
    }
    
    // Update an existing note
    public async Task<bool> UpdateNoteAsync(Guid id, NoteUpdateDto dto)
    {
        var note = await _noteRepository.GetNoteByIdAsync(id);
        if (note is null) return false;
        
        note.Title = dto.Title;
        note.Content = dto.Content;
        // Optionally update the CreatedAt or add an UpdatedAt property
        
        await _noteRepository.UpdateNoteAsync(note);
        return true;
    }
    
    // Delete a note
    public async Task<bool> DeleteNoteAsync(Guid id)
    {
        var note = await _noteRepository.GetNoteByIdAsync(id);
        if (note is null) return false;
        
        await _noteRepository.DeleteNoteAsync(note);
        return true;
    }
}