using MediatR;
using PersonalVault.Application.Notes.DTOs;
using System;

namespace PersonalVault.Application.Notes.Queries
{
    public class GetNoteByIdQuery : IRequest<NoteDto?>
    {
        public Guid Id { get; }
        
        public GetNoteByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}