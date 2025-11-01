using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Tickets.Rules
{
    public class TicketBusinessRules : ITicketBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CheckIfEventExists(int eventId)
        {
            var exists = await _unitOfWork.Events.AnyAsync(eventId);
            if (!exists)
            {
                throw new BusinessException($"ID'si {eventId} olan etkinlik bulunamadı.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfEventHasCapacity(int eventId)
        {
            var eventWithVenue = await _unitOfWork.Events.Where(x => x.Id == eventId)
                .Include(x => x.Venue).AsNoTracking().FirstOrDefaultAsync();

            if (eventWithVenue?.Venue == null)
            {
                throw new BusinessException($"Etkinliğin bağlı olduğu mekan bilgisi bulunamadı.", HttpStatusCode.InternalServerError);
            }

            int capacity = eventWithVenue.Venue.Capacity;

            int soldTickets = await _unitOfWork.Tickets.GetTicketCountByEventAsync(eventId);

            if (soldTickets > capacity)
            {
                throw new BusinessException($"Etkinlik için biletler tükendi. Kapasite: {capacity}.", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfTicketExistsByQrCode(Guid qrCode)
        {
            var exists = await _unitOfWork.Tickets.AnyAsync(x => x.QRCodeGuid == qrCode);
            if (!exists)
            {
                throw new BusinessException($"QR Kodu: ({qrCode}) olan bilet bulunamadı.", HttpStatusCode.NotFound);
            }
        }

        public async Task CheckIfTicketIsAlreadyUsed(Guid qrCode)
        {
            var isUsed = await _unitOfWork.Tickets.AnyAsync(x => x.QRCodeGuid == qrCode && x.IsUsed == true);
            if (isUsed)
            {
                throw new BusinessException($"Bu bilet zaten kullanılmış.", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfUserExists(int userId)
        {
            var exists = await _unitOfWork.Users.AnyAsync(userId);
            if (!exists)
            {
                throw new BusinessException($"ID'si {userId} olan kullanıcı bulunamadı.", HttpStatusCode.BadRequest);
            }
        }
    }
}
