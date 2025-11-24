using FluentValidation;

namespace NexusTix.Application.Features.Tickets.CheckIn
{
    public class CheckInTicketRequestValidator : AbstractValidator<CheckInTicketRequest>
    {
        public CheckInTicketRequestValidator()
        {
            RuleFor(x => x.QRCodeGuid)
                .NotEmpty().WithMessage("QR Kod alanı boş bırakılamaz.")
                .NotNull().WithMessage("QR Kod alanı zorunludur.");
        }
    }
}
