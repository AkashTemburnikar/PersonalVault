using AutoMapper;
using PersonalVault.Application.Common.Interfaces;
using PersonalVault.Application.Notes.DTOs;
using PersonalVault.Domain.Entities;

namespace PersonalVault.Application.Notes.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
    
        public NoteService(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }
    
        public async Task<Guid> CreateNoteAsync(NoteCreateDto dto)
        {
            // Use AutoMapper to map NoteCreateDto to Note entity.
            var note = _mapper.Map<Note>(dto);
            note.Id = Guid.NewGuid();
            note.CreatedAt = DateTime.UtcNow;
            
            return await _noteRepository.AddNoteAsync(note);
        }
    
        public async Task<NoteDto?> GetNoteByIdAsync(Guid id)
        {
            var note = await _noteRepository.GetNoteByIdAsync(id);
            if (note is null)
                return null;
    
            // Use AutoMapper to map entity to DTO.
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
            
            // AutoMapper can map changes from DTO onto the existing note entity
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