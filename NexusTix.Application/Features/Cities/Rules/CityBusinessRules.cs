﻿using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Cities.Rules
{
    public class CityBusinessRules : ICityBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CheckIfCityExists(int cityId)
        {
            var exists = await _unitOfWork.Cities.AnyAsync(cityId);
            if (!exists)
            {
                throw new BusinessException($"ID'si {cityId} olan şehir bulunamadı.", HttpStatusCode.NotFound);
            }
        }

        public async Task CheckIfCityNameExistsWhenCreating(string cityName)
        {
            var exists = await _unitOfWork.Cities.AnyAsync(x => x.Name.ToLower() == cityName.ToLower());
            if (exists)
            {
                throw new BusinessException($"Şehir adı: {cityName}. Bu isimde başka bir şehir mevcut", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfCityNameExistsWhenUpdating(int cityId, string cityName)
        {
            var exists = await _unitOfWork.Cities.AnyAsync(x => x.Name.ToLower() == cityName.ToLower() && x.Id != cityId);
            if (exists)
            {
                throw new BusinessException($"Şehir adı: {cityName}. Bu isimde başka bir şehir mevcut.", HttpStatusCode.Conflict);
            }
        }
    }
}
