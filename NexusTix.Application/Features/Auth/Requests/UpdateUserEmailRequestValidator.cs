using FluentValidation;

namespace NexusTix.Application.Features.Auth.Requests
{
    public class UpdateUserEmailRequestValidator : AbstractValidator<UpdateUserEmailRequest>
    {
        public UpdateUserEmailRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Kullanıcı ID sıfırdan büyük olmalıdır.");

            RuleFor(x => x.NewEmail)
                .NotEmpty().WithMessage("Yeni E-posta alanı boş bırakılamaz.")
                .NotNull().WithMessage("Yeni E-posta alanı gereklidir.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(256).WithMessage("E-posta adresi 256 karakterden uzun olamaz.");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Mevcut şifre alanı boş bırakılamaz.")
                .NotNull().WithMessage("Mevcut şifre alanı gereklidir.");
        }
    }
}
