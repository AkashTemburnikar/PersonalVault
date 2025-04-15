namespace PersonalVault.Application.Notes.DTOs;

public class NoteUpdateDto
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
}