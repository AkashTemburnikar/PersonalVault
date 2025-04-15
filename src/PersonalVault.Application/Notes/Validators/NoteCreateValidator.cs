using FluentValidation;
using PersonalVault.Application.Notes.DTOs;

namespace PersonalVault.Application.Notes.Validators;

public class NoteCreateValidator : AbstractValidator<NoteCreateDto>
{
    public NoteCreateValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(x => x.Content)
            .NotEmpty();
    }
}