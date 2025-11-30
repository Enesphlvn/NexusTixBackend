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
                .NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.")
                .Matches(@"^05\d{9}$")
                .WithMessage("Telefon numarası '05' ile başlamalı ve 11 haneli olmalıdır. (Örn: 05xxxxxxxxx)")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        }
    }
}
