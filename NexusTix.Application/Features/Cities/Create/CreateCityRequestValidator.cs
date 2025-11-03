using FluentValidation;

namespace NexusTix.Application.Features.Cities.Create
{
    public class CreateCityRequestValidator : AbstractValidator<CreateCityRequest>
    {
        public CreateCityRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Şehir adı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Şehir adı 100 karakterden uzun olamaz.")
                .MinimumLength(3).WithMessage("Şehir adı 3 karakterden kısa olamaz.");
        }
    }
}
