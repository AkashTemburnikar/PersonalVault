using System;

namespace PersonalVault.Application.Notes.DTOs;

public class NoteDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}