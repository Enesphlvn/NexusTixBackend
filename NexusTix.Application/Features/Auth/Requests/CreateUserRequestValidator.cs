using FluentValidation;

namespace NexusTix.Application.Features.Auth.Requests
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta alanı boş bırakılamaz.")
                .NotNull().WithMessage("E-posta alanı gereklidir.")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.Net4xRegex)
                    .WithMessage("Geçerli bir e-posta adresi giriniz. (örn: kullanici@alanadi.com).")
                .MaximumLength(256).WithMessage("E-posta adresi 256 karakterden uzun olamaz.");

            RuleFor(x => x.Password)
                 .NotEmpty().WithMessage("Şifre alanı boş bırakılamaz.")
                 .NotNull().WithMessage("Şifre alanı gereklidir.")
                 .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.")
                 .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
                 .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                 .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.")
                 .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.");

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
