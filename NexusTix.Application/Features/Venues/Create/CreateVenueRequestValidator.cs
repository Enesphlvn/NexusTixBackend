using FluentValidation;

namespace NexusTix.Application.Features.Venues.Create
{
    public class CreateVenueRequestValidator : AbstractValidator<CreateVenueRequest>
    {
        public CreateVenueRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Mekan adı boş bırakılamaz.")
                .MaximumLength(150).WithMessage("Mekan adı 150 karakterden uzun olamaz.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Mekan kapasitesi sıfırdan büyük olmalıdır.");

            RuleFor(x => x.DistrictId)
                .GreaterThan(0).WithMessage("District Id sıfırdan büyük olmalıdır.");
        }
    }
}
