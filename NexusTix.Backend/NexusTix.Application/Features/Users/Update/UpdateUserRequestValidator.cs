using FluentValidation;

namespace NexusTix.Application.Features.Users.Update
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Kullanıcı ID sıfırdan büyük olmalıdır.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad alanı boş bırakılamaz.")
                .NotNull().WithMessage("Ad alanı gereklidir.")
                .MaximumLength(100).WithMessage("Ad 100 karakterden uzun olamaz.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad alanı boş bırakılamaz.")
                .NotNull().WithMessage("Soyad alanı gereklidir.")
                .MaximumLength(100).WithMessage("Soyad 100 karakterden uzun olamaz.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[0-9]{7,15}$").WithMessage("Geçerli bir telefon numarası formatı giriniz.")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        }
    }
}
