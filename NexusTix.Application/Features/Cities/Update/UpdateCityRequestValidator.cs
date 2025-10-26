using FluentValidation;

namespace NexusTix.Application.Features.Cities.Update
{
    public class UpdateCityRequestValidator : AbstractValidator<UpdateCityRequest>
    {
        public UpdateCityRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Şehir Id'si sıfırdan büyük olmalıdır.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Şehir adı boş bırakılamaz.")
                .MaximumLength(15).WithMessage("Şehir adı 15 karakterden uzun olamaz.")
                .MinimumLength(3).WithMessage("Şehir adı 3 karakterden kısa olamaz.");
        }
    }
}
