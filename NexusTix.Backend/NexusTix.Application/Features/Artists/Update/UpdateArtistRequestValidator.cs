using FluentValidation;

namespace NexusTix.Application.Features.Artists.Update
{
    internal class UpdateArtistRequestValidator : AbstractValidator<UpdateArtistRequest>
    {
        public UpdateArtistRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Sanatçı ID'si 0'dan büyük olmalıdır.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Sanatçı adı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Sanatçı adı 100 karakterden uzun olamaz.")
                .MinimumLength(2).WithMessage("Sanatçı adı en az 2 karakter olmalıdır.");

            RuleFor(x => x.Bio)
                .MaximumLength(1000).WithMessage("Biyografi 1000 karakterden uzun olamaz.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(500).WithMessage("Resim yolu 500 karakterden uzun olamaz.");
        }
    }
}
