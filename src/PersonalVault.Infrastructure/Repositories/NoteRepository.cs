using Microsoft.EntityFrameworkCore;
using PersonalVault.Application.Common.Interfaces;
using PersonalVault.Domain.Entities;
using PersonalVault.Infrastructure.Persistence;

namespace PersonalVault.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly AppDbContext _dbContext;

    public NoteRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> AddNoteAsync(Note note)
    {
        _dbContext.Notes.Add(note);
        await _dbContext.SaveChangesAsync();
        return note.Id;
    }
    
    public async Task<Note?> GetNoteByIdAsync(Guid id)
    {
        return await _dbContext.Notes.FindAsync(id);
    }
    
    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        return await _dbContext.Notes.ToListAsync();
    }
    
    // Update the note
    public async Task UpdateNoteAsync(Note note)
    {
        _dbContext.Notes.Update(note);
        await _dbContext.SaveChangesAsync();
    }
    
    // Delete the note
    public async Task DeleteNoteAsync(Note note)
    {
        _dbContext.Notes.Remove(note);
        await _dbContext.SaveChangesAsync();
    }
}