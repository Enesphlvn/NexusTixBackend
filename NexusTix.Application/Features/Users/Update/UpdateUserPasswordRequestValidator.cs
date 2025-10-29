using FluentValidation;

namespace NexusTix.Application.Features.Users.Update
{
    public class UpdateUserPasswordRequestValidator : AbstractValidator<UpdateUserPasswordRequest>
    {
        public UpdateUserPasswordRequestValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Kullanıcı ID sıfırdan büyük olmalıdır.");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Mevcut şifre alanı boş bırakılamaz.")
                .NotNull().WithMessage("Mevcut şifre alanı gereklidir.");

            RuleFor(x => x.NewPassword)
                 .NotEmpty().WithMessage("Yeni şifre alanı boş bırakılamaz.")
                 .NotNull().WithMessage("Yeni şifre alanı gereklidir.")
                 .MinimumLength(6).WithMessage("Yeni şifre en az 6 karakter olmalıdır.")
                 .Matches("[0-9]").WithMessage("Yeni şifre en az bir rakam içermelidir.")
                 .Matches("[A-Z]").WithMessage("Yeni şifre en az bir büyük harf içermelidir.")
                 .Matches("[^a-zA-Z0-9]").WithMessage("Yeni şifre en az bir özel karakter içermelidir.")
                 .Matches("[a-z]").WithMessage("Yeni şifre en az bir küçük harf içermelidir.");

            RuleFor(x => x.NewPasswordConfirm)
                .Equal(x => x.NewPassword).WithMessage("Yeni şifre ile onay şifresi eşleşmiyor.");

            RuleFor(x => x.NewPassword)
                .NotEqual(x => x.CurrentPassword)
                .WithMessage("Yeni şifreniz mevcut şifrenizle aynı olamaz!");
        }
    }
}
