namespace PersonalVault.Application.Notes.DTOs;

public class NoteCreateDto
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
}