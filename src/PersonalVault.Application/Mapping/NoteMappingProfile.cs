using AutoMapper;
using PersonalVault.Application.Notes.DTOs;
using PersonalVault.Domain.Entities;

namespace PersonalVault.Application.Mapping
{
    public class NoteMappingProfile : Profile
    {
        public NoteMappingProfile()
        {
            // Map from Note to NoteDto
            CreateMap<Note, NoteDto>();
            
            // Map from NoteCreateDto to Note. For created notes, let the service set properties such as Id and CreatedAt.
            CreateMap<NoteCreateDto, Note>();
            
            // Map from NoteUpdateDto to Note
            CreateMap<NoteUpdateDto, Note>();
        }
    }
}