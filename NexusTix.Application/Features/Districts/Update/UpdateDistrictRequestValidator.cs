using FluentValidation;

namespace NexusTix.Application.Features.Districts.Update
{
    public class UpdateDistrictRequestValidator : AbstractValidator<UpdateDistrictRequest>
    {
        public UpdateDistrictRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("İlçe Id'si sıfırdan büyük olmalıdır.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İlçe adı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("İlçe adı 100 karakterden uzun olamaz.");

            RuleFor(x => x.CityId)
                .GreaterThan(0).WithMessage("City Id sıfırdan büyük olmalıdır.");
        }
    }
}
