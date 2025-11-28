using NexusTix.Application.Common.BaseRules;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Events.Rules
{
    public class EventBusinessRules : BaseBusinessRules, IEventBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CheckIfArtistExists(int artistId)
        {
            var exists = await _unitOfWork.Artists.AnyAsync(artistId);
            if (!exists)
            {
                throw new BusinessException($"ID'si '{artistId}' olan sanatçı bulunamadı.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfCityExists(int cityId)
        {
            var exists = await _unitOfWork.Cities.AnyAsync(cityId);
            if (!exists)
            {
                throw new BusinessException($"ID'si '{cityId}' olan şehir bulunamadı.", HttpStatusCode.BadRequest);
            }
        }

        public void CheckIfDateRangeIsValid(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            if (startDate > endDate)
            {
                throw new BusinessException("Başlangıç tarihi, bitiş tarihinden önce olmalıdır.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfDistrictExists(int districtId)
        {
            var exists = await _unitOfWork.Districts.AnyAsync(districtId);
            if (!exists)
            {
                throw new BusinessException($"ID'si '{districtId}' olan ilçe bulunamadı.", HttpStatusCode.BadRequest);
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

        public async Task CheckIfEventHasNoTickets(int eventId)
        {
            var hasActiveTickets = await _unitOfWork.Tickets.AnyAsync(x => x.EventId == eventId && !x.IsCancelled);
            if (hasActiveTickets)
            {
                throw new BusinessException($"ID'si '{eventId}' olan etkinliğe ait aktif biletler mevcuttur! İşlem gerçekleştirilemez.", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfEventNameExistsWhenCreating(string eventName)
        {
            var exists = await _unitOfWork.Events.AnyAsync(x => x.Name.ToLower() == eventName.ToLower());
            if (exists)
            {
                throw new BusinessException($"Etkinlik adı: '{eventName}'. Bu isimde başka bir etkinlik mevcut", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfEventNameExistsWhenUpdating(int eventId, string eventName)
        {
            var exists = await _unitOfWork.Events.AnyAsync(x => x.Name.ToLower() == eventName.ToLower() && x.Id != eventId);
            if (exists)
            {
                throw new BusinessException($"Etkinlik adı: '{eventName}'. Bu isimde başka bir etkinlik mevcut", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfEventTypeExists(int eventTypeId)
        {
            var exists = await _unitOfWork.EventTypes.AnyAsync(eventTypeId);
            if (!exists)
            {
                throw new BusinessException($"ID'si '{eventTypeId}' olan etkinlik tipi bulunamadı.", HttpStatusCode.BadRequest);
            }
        }

        public void CheckIfNumberOfEventsIsValid(int numberOfEvents)
        {
            if (numberOfEvents <= 0 || numberOfEvents > 100)
            {
                throw new BusinessException("İstenen etkinlik sayısı 1 ile 100 arasında olmalıdır.", HttpStatusCode.BadRequest);
            }
        }

        public void CheckIfPriceRangeIsValid(decimal minPrice, decimal maxPrice)
        {
            if (minPrice > maxPrice)
            {
                throw new BusinessException("Minimum fiyat, maksimum fiyattan büyük olamaz.", HttpStatusCode.BadRequest);
            }

            if (minPrice < 0)
            {
                throw new BusinessException("Fiyatlar negatif olamaz.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfUserExists(int userId)
        {
            var exists = await _unitOfWork.Users.AnyAsync(userId);
            if (!exists)
            {
                throw new BusinessException($"ID'si '{userId}' olan kullanıcı bulunamadı.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfVenueExists(int venueId)
        {
            var exists = await _unitOfWork.Venues.AnyAsync(venueId);
            if (!exists)
            {
                throw new BusinessException($"ID'si '{venueId}' olan mekan bulunamadı.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfVenueHasEnoughCapacity(int venueId, int eventCapacity)
        {
            var venue = await _unitOfWork.Venues.GetByIdAsync(venueId);

            if (venue == null) throw new BusinessException("Mekan bulunamadı.", HttpStatusCode.NotFound);

            if (eventCapacity > venue.Capacity)
            {
                throw new BusinessException($"Etkinlik kapasitesi '{eventCapacity}', seçilen mekanın kapasitesini '{venue.Capacity}' aşamaz.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfVenueIsAvailableOnDateCreating(int venueId, DateTimeOffset date)
        {
            var exists = await _unitOfWork.Events.AnyAsync(x => x.VenueId == venueId && x.Date.Date == date.Date);
            if (exists)
            {
                throw new BusinessException($"ID'si '{venueId}' olan mekan, '{date:dd/MM/yyyy}' tarihinde başka bir etkinliğe sahiptir.", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfVenueIsAvailableOnDateUpdating(int eventId, int venueId, DateTimeOffset date)
        {
            var exists = await _unitOfWork.Events.AnyAsync(x => x.VenueId == venueId && x.Date.Date == date.Date && x.Id != eventId);
            if (exists)
            {
                throw new BusinessException($"ID'si '{venueId}' olan mekan, '{date:dd/MM/yyyy}' tarihinde başka bir etkinliğe sahiptir.", HttpStatusCode.Conflict);
            }
        }
    }
}
