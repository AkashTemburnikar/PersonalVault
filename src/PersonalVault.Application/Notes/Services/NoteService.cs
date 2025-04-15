using AutoMapper;
using MediatR;
using PersonalVault.Application.Common.Interfaces;
using PersonalVault.Application.Notes.DTOs;
using PersonalVault.Domain.Entities;
using PersonalVault.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalVault.Application.Notes.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;  // Inject MediatR for publishing domain events

        public NoteService(INoteRepository noteRepository, IMapper mapper, IMediator mediator)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Guid> CreateNoteAsync(NoteCreateDto dto)
        {
            // Map DTO to entity
            var note = _mapper.Map<Note>(dto);
            note.Id = Guid.NewGuid();
            note.CreatedAt = DateTime.UtcNow;
            
            // Add note to repository/persistence
            var noteId = await _noteRepository.AddNoteAsync(note);
            
            // Publish the domain event
            await _mediator.Publish(new NoteCreatedEvent(note.Id, note.Title, note.CreatedAt));
            
            return noteId;
        }

        public async Task<NoteDto?> GetNoteByIdAsync(Guid id)
        {
            var note = await _noteRepository.GetNoteByIdAsync(id);
            if (note is null)
                return null;
            return _mapper.Map<NoteDto>(note);
        }

        public async Task<IEnumerable<NoteDto>> GetAllNotesAsync()
        {
            var notes = await _noteRepository.GetAllNotesAsync();
            return _mapper.Map<IEnumerable<NoteDto>>(notes);
        }

        public async Task<bool> UpdateNoteAsync(Guid id, NoteUpdateDto dto)
        {
            var note = await _noteRepository.GetNoteByIdAsync(id);
            if (note is null) return false;

            // Map changes from dto onto the existing entity.
            _mapper.Map(dto, note);
            await _noteRepository.UpdateNoteAsync(note);
            return true;
        }

        public async Task<bool> DeleteNoteAsync(Guid id)
        {
            var note = await _noteRepository.GetNoteByIdAsync(id);
            if (note is null) return false;
            await _noteRepository.DeleteNoteAsync(note);
            return true;
        }
    }
}