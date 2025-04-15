using Microsoft.AspNetCore.Mvc;
using PersonalVault.Application.Notes.DTOs;
using PersonalVault.Application.Notes.Services;

namespace PersonalVault.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;
    
    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }
    
    // POST: api/notes
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NoteCreateDto noteDto)
    {
        var noteId = await _noteService.CreateNoteAsync(noteDto);
        return CreatedAtAction(nameof(GetById), new { id = noteId }, new { id = noteId });
    }
    
    // GET: api/notes/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var note = await _noteService.GetNoteByIdAsync(id);
        if (note is null)
            return NotFound();
        return Ok(note);
    }
    
    // GET: api/notes
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var notes = await _noteService.GetAllNotesAsync();
        return Ok(notes);
    }
}