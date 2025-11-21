using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NexusTix.Application.Common.BaseRules;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Tickets.Rules
{
    public class TicketBusinessRules : BaseBusinessRules, ITicketBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CheckIfDateRangeIsValid(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            if (startDate > endDate)
            {
                throw new BusinessException("Başlangıç tarihi, bitiş tarihinden önce olmalıdır.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfEventExists(int eventId)
        {
            var exists = await _unitOfWork.Events.AnyAsync(eventId);
            if (!exists)
            {
                throw new BusinessException($"ID'si '{eventId}' olan etkinlik bulunamadı.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfEventHasCapacity(int eventId)
        {
            var eventEntity = await _unitOfWork.Events.Where(x => x.Id == eventId)
                .Select(x => new { x.Capacity }).AsNoTracking().FirstOrDefaultAsync();

            if (eventEntity == null)
            {
                throw new BusinessException($"Etkinlik bilgisi bulunamadı.", HttpStatusCode.NotFound);
            }

            int capacity = eventEntity.Capacity;

            int soldTickets = await _unitOfWork.Tickets.GetTicketCountByEventAsync(eventId);

            if (soldTickets >= capacity)
            {
                throw new BusinessException($"Etkinlik için biletler tükendi. Kapasite: '{capacity}'.", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfEventIsPast(int eventId)
        {
            var eventDate = await _unitOfWork.Events
                .Where(x => x.Id == eventId)
                .Select(x => x.Date)
                .FirstOrDefaultAsync();

            if (eventDate < DateTimeOffset.UtcNow)
            {
                throw new BusinessException("Geçmiş tarihli etkinliklere bilet satın alınamaz.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfTicketCanBeCancelled(int ticketId)
        {
            var ticket = await _unitOfWork.Tickets.Where(x => x.Id == ticketId)
                .Include(x => x.Event).AsNoTracking().FirstOrDefaultAsync();

            if (ticket!.IsCancelled)
            {
                throw new BusinessException("Bu bilet zaten iptal edilmiş.", HttpStatusCode.BadRequest);
            }

            if (ticket.IsUsed)
            {
                throw new BusinessException("Kullanılmış biletler iptal edilemez.", HttpStatusCode.BadRequest);
            }

            if (DateTimeOffset.UtcNow > ticket.Event.Date)
            {
                throw new BusinessException("Geçmiş etkinliklerin biletleri iptal edilemez.", HttpStatusCode.BadRequest);
            }

            if (DateTimeOffset.UtcNow.AddHours(24) > ticket.Event.Date)
            {
                throw new BusinessException("Etkinliğe 24 saat kala bilet iptal edilemez.", HttpStatusCode.BadRequest);

            }
        }

        public async Task CheckIfTicketExists(int ticketId)
        {
            var exists = await _unitOfWork.Tickets.AnyAsync(ticketId);
            if (!exists)
            {
                throw new BusinessException($"ID'si '{ticketId}' olan bilet bulunamadı.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfTicketExistsByQrCode(Guid qrCode)
        {
            var exists = await _unitOfWork.Tickets.AnyAsync(x => x.QRCodeGuid == qrCode);
            if (!exists)
            {
                throw new BusinessException($"QR Kodu: '{qrCode}' olan bilet bulunamadı.", HttpStatusCode.NotFound);
            }
        }

        public async Task CheckIfTicketIsAlreadyUsed(Guid qrCode)
        {
            var ticket = await _unitOfWork.Tickets.Where(x => x.QRCodeGuid == qrCode).FirstOrDefaultAsync();

            if (ticket!.IsUsed) throw new BusinessException("Bilet zaten kullanılmış.", HttpStatusCode.Conflict);

            if (ticket.IsCancelled) throw new BusinessException("Bu bilet İPTAL EDİLMİŞTİR. Giriş yapılamaz.", HttpStatusCode.Conflict);
        }

        public async Task CheckIfUserExists(int userId)
        {
            var exists = await _unitOfWork.Users.AnyAsync(userId);
            if (!exists)
            {
                throw new BusinessException($"ID'si '{userId}' olan kullanıcı bulunamadı.", HttpStatusCode.BadRequest);
            }
        }
    }
}
