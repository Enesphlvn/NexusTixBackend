﻿using NexusTix.Domain.Entities;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Events.Rules
{
    public class EventBusinessRules : IEventBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventBusinessRules(IUnitOfWork unitOfWork)
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
                throw new BusinessException($"ID'si {eventId} olan etkinlik bulunamadı.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfEventHasNoTickets(int eventId)
        {
            var hasTickets = await _unitOfWork.Tickets.AnyAsync(x => x.EventId == eventId);
            if (hasTickets)
            {
                throw new BusinessException($"ID'si {eventId} olan etkinliğe satılmış biletler mevcuttur! İşlem gerçekleştirilemez.", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfEventNameExistsWhenCreating(string eventName)
        {
            var exists = await _unitOfWork.Events.AnyAsync(x => x.Name.ToLower() == eventName.ToLower());
            if (exists)
            {
                throw new BusinessException($"Etkinlik adı: {eventName}. Bu isimde başka bir etkinlik mevcut", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfEventNameExistsWhenUpdating(int eventId, string eventName)
        {
            var exists = await _unitOfWork.Events.AnyAsync(x => x.Name.ToLower() == eventName.ToLower() && x.Id != eventId);
            if (exists)
            {
                throw new BusinessException($"Etkinlik adı: {eventName}. Bu isimde başka bir etkinlik mevcut", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfEventTypeExists(int eventTypeId)
        {
            var exists = await _unitOfWork.EventTypes.AnyAsync(eventTypeId);
            if (!exists)
            {
                throw new BusinessException($"ID'si {eventTypeId} olan etkinlik tipi bulunamadı.", HttpStatusCode.BadRequest);
            }
        }

        public void CheckIfNumberOfEventsIsValid(int numberOfEvents)
        {
            if (numberOfEvents <= 0 || numberOfEvents > 100)
            {
                throw new BusinessException("İstenen etkinlik sayısı 1 ile 100 arasında olmalıdır.", HttpStatusCode.BadRequest);
            }
        }

        public void CheckIfPagingParametersAreValid(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new BusinessException("Sayfa numarası veya boyutu sıfırdan büyük olmalıdır.", HttpStatusCode.BadRequest);
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
                throw new BusinessException($"ID'si {userId} olan kullanıcı bulunamadı.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfVenueExists(int venueId)
        {
            var exists = await _unitOfWork.Venues.AnyAsync(venueId);
            if (!exists)
            {
                throw new BusinessException($"ID'si {venueId} olan mekan bulunamadı.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfVenueIsAvailableOnDateCreating(int venueId, DateTimeOffset date)
        {
            var exists = await _unitOfWork.Events.AnyAsync(x => x.VenueId == venueId && x.Date.UtcDateTime.Date == date.UtcDateTime.Date);
            if (exists)
            {
                throw new BusinessException($"ID'si {venueId} olan mekan, {date:dd/MM/yyyy} tarihinde başka bir etkinliğe sahiptir.", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfVenueIsAvailableOnDateUpdating(int eventId, int venueId, DateTimeOffset date)
        {
            var exists = await _unitOfWork.Events.AnyAsync(x => x.VenueId == venueId && x.Date.UtcDateTime.Date == date.UtcDateTime.Date && x.Id != eventId);
            if (exists)
            {
                throw new BusinessException($"ID'si {venueId} olan mekan, {date:dd/MM/yyyy} tarihinde başka bir etkinliğe sahiptir.", HttpStatusCode.Conflict);
            }
        }
    }
}
