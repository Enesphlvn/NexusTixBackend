using FluentValidation;

namespace NexusTix.Application.Features.Auth.Requests
{
    public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta alanı boş bırakılamaz.")
                .NotNull().WithMessage("E-posta alanı gereklidir.")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.Net4xRegex)
                    .WithMessage("Geçerli bir e-posta adresi giriniz. (örn: kullanici@alanadi.com).")
                .MaximumLength(256).WithMessage("E-posta adresi 256 karakterden uzun olamaz.");
        }
    }
}
