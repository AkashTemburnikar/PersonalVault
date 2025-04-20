using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PersonalVault.Application.Notes.DTOs;
using PersonalVault.Application.Notes.Commands;
using PersonalVault.Application.Notes.Queries;

namespace PersonalVault.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/notes - Create a new note.
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NoteCreateDto noteDto)
        {
            var noteId = await _mediator.Send(new CreateNoteCommand(noteDto));
            return CreatedAtAction(nameof(GetById), new { id = noteId }, new { id = noteId });
        }

        // GET: api/notes/{id} - Retrieve a specific note by its ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var note = await _mediator.Send(new GetNoteByIdQuery(id));
            if (note is null)
                return NotFound();
            return Ok(note);
        }

        // GET: api/notes - Retrieve all notes.
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notes = await _mediator.Send(new GetAllNotesQuery());
            return Ok(notes);
        }

        // PUT: api/notes/{id} - Update an existing note.
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] NoteUpdateDto updateDto)
        {
            var result = await _mediator.Send(new UpdateNoteCommand(id, updateDto));
            if (!result)
                return NotFound();
            return NoContent();
        }

        // DELETE: api/notes/{id} - Delete a note.
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteNoteCommand(id));
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}