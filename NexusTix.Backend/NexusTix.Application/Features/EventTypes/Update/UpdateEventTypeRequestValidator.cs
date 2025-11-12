using FluentValidation;

namespace NexusTix.Application.Features.EventTypes.Update
{
    public class UpdateEventTypeRequestValidator : AbstractValidator<UpdateEventTypeRequest>
    {
        public UpdateEventTypeRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Etkinlik türü Id'si sıfırdan büyük olmalıdır.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Etkinlik türü adı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Etkinlik türü adı 50 karakterden uzun olamaz.")
                .MinimumLength(3).WithMessage("Etkinlik türü adı 3 karakterden kısa olamaz.");
        }
    }
}
