using FluentValidation;

namespace NexusTix.Application.Features.Events.Create
{
    public class CreateEventRequestValidator : AbstractValidator<CreateEventRequest>
    {
        public CreateEventRequestValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Etkinlik adı boş bırakılamaz.")
            .MinimumLength(3).WithMessage("Etkinlik adı en az 3 karakter olmalıdır.")
            .MaximumLength(200).WithMessage("Etkinlik adı 200 karakterden uzun olamaz.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Etkinlik tarihi belirtilmelidir.")
                .GreaterThanOrEqualTo(DateTimeOffset.UtcNow.Date)
                .WithMessage("Etkinlik tarihi geçmiş bir tarih olamaz.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Bilet fiyatı sıfırdan büyük olmalıdır.");

            RuleFor(x => x.EventTypeId)
                .NotEmpty().WithMessage("EventType Id 0'dan büyük olmalıdır.")
                .GreaterThan(0).WithMessage("Geçersiz EventType ID'si.");

            RuleFor(x => x.VenueId)
                .NotEmpty().WithMessage("Venue Id 0'dan büyük olmalıdır.")
                .GreaterThan(0).WithMessage("Geçersiz Venue ID'si.");

            RuleFor(x => x.Description)
                .MaximumLength(800).WithMessage("Açıklama 800 karakterden uzun olamaz.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Etkinlik kapasitesi sıfırdan büyük olmalıdır.");

            RuleFor(x => x.ArtistIds)
            .NotNull().WithMessage("Sanatçı listesi boş olamaz (En azından boş bir liste gönderilmelidir).")
            .Must(ids => ids == null || ids.Distinct().Count() == ids.Count())
            .WithMessage("Aynı sanatçı birden fazla kez seçilemez.");
        }
    }
}