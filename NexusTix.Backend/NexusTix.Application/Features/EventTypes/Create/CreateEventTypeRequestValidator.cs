using FluentValidation;

namespace NexusTix.Application.Features.EventTypes.Create
{
    public class CreateEventTypeRequestValidator : AbstractValidator<CreateEventTypeRequest>
    {
        public CreateEventTypeRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Etkinlik türü adı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Etkinlik türü adı 50 karakterden uzun olamaz.")
                .MinimumLength(3).WithMessage("Etkinlik türü adı 3 karakterden kısa olamaz.");
        }
    }
}
