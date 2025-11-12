using FluentValidation;

namespace NexusTix.Application.Features.Tickets.Create
{
    public class CreateTicketRequestValidator : AbstractValidator<CreateTicketRequest>
    {
        public CreateTicketRequestValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Event Id sıfırdan büyük olmalıdır.");
        }
    }
}
