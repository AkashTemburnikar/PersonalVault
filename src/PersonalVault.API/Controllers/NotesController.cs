using Microsoft.AspNetCore.Mvc;
using PersonalVault.Application.Notes.DTOs;
using PersonalVault.Application.Notes.Services;

namespace PersonalVault.API.Controllers
{
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
    
        // PUT: api/notes/{id} - Update note
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] NoteUpdateDto updateDto)
        {
            // Optionally, you could also check ModelState.IsValid here,
            // but with [ApiController] and FluentValidation auto-validation, it's done automatically.
            var success = await _noteService.UpdateNoteAsync(id, updateDto);
            if (!success)
                return NotFound();
            return NoContent();
        }
    
        // DELETE: api/notes/{id} - Delete note
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _noteService.DeleteNoteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}