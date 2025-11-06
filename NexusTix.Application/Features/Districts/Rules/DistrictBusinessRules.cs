using NexusTix.Application.Common.BaseRules;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Districts.Rules
{
    public class DistrictBusinessRules : BaseBusinessRules, IDistrictBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public DistrictBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CheckIfCityExists(int cityId)
        {
            var exists = await _unitOfWork.Cities.AnyAsync(cityId);
            if (!exists)
            {
                throw new BusinessException($"ID'si '{cityId}' olan şehir bulunamadı.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfDistrictExists(int districtId)
        {
            var exists = await _unitOfWork.Districts.AnyAsync(districtId);
            if (!exists)
            {
                throw new BusinessException($"ID'si '{districtId}' olan ilçe bulunamadı.", HttpStatusCode.NotFound);
            }
        }

        public async Task CheckIfDistrictHasNoVenues(int districtId)
        {
            var hasVenues = await _unitOfWork.Venues.AnyAsync(x => x.DistrictId == districtId);
            if (hasVenues)
            {
                throw new BusinessException($"ID'si '{districtId}' olan ilçeye kayıtlı mekanlar bulunmaktadır! İşlem gerçekleştirilemez.", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfDistrictNameExistsWhenCreating(string districtName)
        {
            var exists = await _unitOfWork.Districts.AnyAsync(x => x.Name.ToLower() == districtName.ToLower());
            if (exists)
            {
                throw new BusinessException($"İlçe adı: '{districtName}'. Bu isimde başka bir ilçe mevcut", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfDistrictNameExistsWhenUpdating(int districtId, string districtName)
        {
            var exists = await _unitOfWork.Districts.AnyAsync(x => x.Name.ToLower() == districtName.ToLower() && x.Id != districtId);
            if (exists)
            {
                throw new BusinessException($"İlçe adı: '{districtName}'. Bu isimde başka bir ilçe mevcut", HttpStatusCode.Conflict);
            }
        }
    }
}
