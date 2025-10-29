using Microsoft.AspNetCore.Identity;
using NexusTix.Domain.Entities;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Users.Rules
{
    public class UserBusinessRules : IUserBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public UserBusinessRules(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
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

        public async Task CheckIfEmailExistsWhenCreating(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                throw new BusinessException($"Email: {email}. Bu email zaten kullanımda", HttpStatusCode.Conflict);
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

        public void CheckIfNewPasswordIsDifferent(string currentPassword, string newPassword)
        {
            if (currentPassword == newPassword)
            {
                throw new BusinessException("Yeni şifre, şu an ki şifreyle aynı olamaz", HttpStatusCode.Conflict);
            }
        }

        public void CheckIfPagingParametersAreValid(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new BusinessException("Sayfa numarası veya boyutu sıfırdan büyük olmalıdır.", HttpStatusCode.BadRequest);
            }
        }

        public async Task CheckIfPhoneNumberExists(string phoneNumber)
        {
            var exists = await _unitOfWork.Users.AnyAsync(x => x.PhoneNumber == phoneNumber && x.IsActive);
            if (exists)
            {
                throw new BusinessException($"Telefon numarası: {phoneNumber}. Bu numara zaten kullanımda", HttpStatusCode.Conflict);
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
    }
}
