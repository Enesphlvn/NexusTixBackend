using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NexusTix.Application.Common.BaseRules;
using NexusTix.Domain.Entities;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Users.Rules
{
    public class UserBusinessRules : BaseBusinessRules, IUserBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public UserBusinessRules(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CheckIfPhoneNumberExistsWhenUpdating(int userId, string phoneNumber)
        {
            var exists = await _unitOfWork.Users.AnyAsync(x => x.PhoneNumber == phoneNumber && x.IsActive && x.Id != userId);
            if (exists)
            {
                throw new BusinessException($"Telefon numarası: '{phoneNumber}'. Bu numara zaten kullanımda", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfRoleExists(string roleName)
        {
            var exists = await _roleManager.RoleExistsAsync(roleName);
            if (!exists)
            {
                throw new BusinessException($"'{roleName}' adında bir rol bulunamadı.", HttpStatusCode.NotFound);
            }
        }

        public async Task CheckIfUserExists(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new BusinessException($"ID'si '{userId}' olan kullanıcı bulunamadı.", HttpStatusCode.NotFound);
            }
        }

        public async Task CheckIfUserHasActiveFutureTickets(int userId)
        {
            var hasActiveTickets = await _unitOfWork.Tickets.AnyAsync(x => x.UserId == userId && !x.IsCancelled && x.Event.Date > DateTimeOffset.UtcNow);

            if (hasActiveTickets)
            {
                throw new BusinessException($"Bu kullanıcının gelecekteki etkinlikler için aktif biletleri bulunmaktadır. Kullanıcı pasife alınamaz.", HttpStatusCode.Conflict);
            }
        }
    }
}
