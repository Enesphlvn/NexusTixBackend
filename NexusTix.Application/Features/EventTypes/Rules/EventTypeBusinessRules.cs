using NexusTix.Application.Common.BaseRules;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.EventTypes.Rules
{
    public class EventTypeBusinessRules : BaseBusinessRules, IEventTypeBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventTypeBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CheckIfEventTypeExists(int eventTypeId)
        {
            var exists = await _unitOfWork.EventTypes.AnyAsync(x => x.Id == eventTypeId);
            if (!exists)
            {
                throw new BusinessException($"ID'si {eventTypeId} olan etkinlik türü bulunamadı.", HttpStatusCode.NotFound);
            }
        }

        public async Task CheckIfEventTypeHasNoEvents(int eventTypeId)
        {
            var hasEvents = await _unitOfWork.Events.AnyAsync(x => x.EventTypeId == eventTypeId);
            if (hasEvents)
            {
                throw new BusinessException($"ID'si {eventTypeId} olan etkinlik türüne ait kayıtlı etkinlikler mevcuttur! Silinemez.", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfEventTypeNameExistsWhenCreating(string eventTypeName)
        {
            var exists = await _unitOfWork.EventTypes.AnyAsync(x => x.Name.ToLower() == eventTypeName.ToLower());
            if (exists)
            {
                throw new BusinessException($"Etkinlik türü adı: {eventTypeName}. Bu isimde başka bir etkinlik türü mevcut", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfEventTypeNameExistsWhenUpdating(int eventTypeId, string eventTypeName)
        {
            var exists = await _unitOfWork.EventTypes.AnyAsync(x => x.Name.ToLower() == eventTypeName.ToLower() && x.Id != eventTypeId);
            if (exists)
            {
                throw new BusinessException($"Etkinlik türü adı: {eventTypeName}. Bu isimde başka bir etkinlik türü mevcut", HttpStatusCode.Conflict);
            }
        }
    }
}
