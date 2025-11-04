using Microsoft.AspNetCore.Identity;
using NexusTix.Domain.Entities;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Auth.Rules
{
    public class AuthBusinessRules : IAuthBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AuthBusinessRules(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CheckIfEmailExistsWhenCreating(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                throw new BusinessException($"Email: {email}. Bu email zaten kullanımda", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfPhoneNumberExistsWhenCreating(string phoneNumber)
        {
            var exists = await _unitOfWork.Users.AnyAsync(x => x.PhoneNumber == phoneNumber && x.IsActive);
            if (exists)
            {
                throw new BusinessException($"Telefon numarası: {phoneNumber}. Bu numara zaten kullanımda", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfEmailExistsWhenUpdating(int userId, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && user.Id != userId)
            {
                throw new BusinessException($"Email: {email}. Bu email zaten kullanımda", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfUserExists(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new BusinessException($"ID'si {userId} olan kullanıcı bulunamadı.", HttpStatusCode.NotFound);
            }
        }

        public async Task CheckIfCurrentPasswordIsValid(int userId, string currentPassword)
        {
            await CheckIfUserExists(userId);

            var user = await _userManager.FindByIdAsync(userId.ToString());

            var isPasswordValid = await _userManager.CheckPasswordAsync(user!, currentPassword);
            if (!isPasswordValid)
            {
                throw new BusinessException("Mevcut şifre geçerli değil.", HttpStatusCode.BadRequest);
            }
        }

        public void CheckIfNewPasswordIsDifferent(string currentPassword, string newPassword)
        {
            if (currentPassword == newPassword)
            {
                throw new BusinessException("Yeni şifre, şu an ki şifreyle aynı olamaz", HttpStatusCode.Conflict);
            }
        }
    }
}
