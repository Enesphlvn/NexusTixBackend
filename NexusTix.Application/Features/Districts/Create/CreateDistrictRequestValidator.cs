using FluentValidation;

namespace NexusTix.Application.Features.Districts.Create
{
    public class CreateDistrictRequestValidator : AbstractValidator<CreateDistrictRequest>
    {
        public CreateDistrictRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İlçe adı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("İlçe adı 100 karakterden uzun olamaz.");

            RuleFor(x => x.CityId)
                .GreaterThan(0).WithMessage("CityId 0'dan büyük olmalıdır.");
        }
    }
}
