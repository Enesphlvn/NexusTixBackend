using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Venues.Rules
{
    public class VenueBusinessRules : IVenueBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public VenueBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CheckIfDistrictExists(int districtId)
        {
            var exists = await _unitOfWork.Districts.AnyAsync(districtId);
            if (!exists)
            {
                throw new BusinessException($"ID'si {districtId} olan ilçe bulunamadı.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfVenueExists(int venueId)
        {
            var exists = await _unitOfWork.Venues.AnyAsync(venueId);
            if (!exists)
            {
                throw new BusinessException($"ID'si {venueId} olan mekan bulunamadı.", HttpStatusCode.NotFound);
            }
        }

        public async Task CheckIfVenueNameExistsWhenCreating(string venueName)
        {
            var exists = await _unitOfWork.Venues.AnyAsync(x => x.Name.ToLower() == venueName.ToLower());
            if (exists)
            {
                throw new BusinessException($"Mekan adı: {venueName}. Bu isimde başka bir mekan mevcut", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfVenueNameExistsWhenUpdating(int venueId, string venueName)
        {
            var exists = await _unitOfWork.Venues.AnyAsync(x => x.Name.ToLower() == venueName.ToLower() && x.Id != venueId);
            if (exists)
            {
                throw new BusinessException("Aynı isimde başka bir mekan mevcut.", HttpStatusCode.Conflict);
            }
        }
    }
}
