using FluentValidation;

namespace NexusTix.Application.Features.Venues.Update
{
    public class UpdateVenueRequestValidator : AbstractValidator<UpdateVenueRequest>
    {
        public UpdateVenueRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Mekan Id'si sıfırdan büyük olmalıdır.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Mekan adı boş bırakılamaz.")
                .MaximumLength(150).WithMessage("Mekan adı 100 karakterden uzun olamaz.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Mekan kapasitesi sıfırdan büyük olmalıdır.");

            RuleFor(x => x.DistrictId)
                .GreaterThan(0).WithMessage("District Id sıfırdan büyük olmalıdır.");
        }
    }
}
