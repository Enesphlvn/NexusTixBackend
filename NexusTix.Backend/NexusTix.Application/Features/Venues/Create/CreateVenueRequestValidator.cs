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

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90)
                .WithMessage("Enlem değeri -90 ile +90 arasında olmalıdır.");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180)
                .WithMessage("Boylam değeri -180 ile +180 arasında olmalıdır.");
        }
    }
}
