using FluentValidation;
using PersonalVault.Application.Notes.DTOs;

namespace PersonalVault.Application.Notes.Validators;

public class NoteUpdateValidator : AbstractValidator<NoteUpdateDto>
{
    public NoteUpdateValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty")
            .MaximumLength(100);
        
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content cannot be empty");
    }
}