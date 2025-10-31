using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NexusTix.Application.Features.Users.Create;
using NexusTix.Application.Features.Users.Responses;
using NexusTix.Application.Features.Users.Rules;
using NexusTix.Application.Features.Users.Update;
using NexusTix.Domain.Entities;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserBusinessRules _userRules;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IUserBusinessRules userRules, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRules = userRules;
            _userManager = userManager;
        }

        public async Task<ServiceResult<UserResponse>> CreateAsync(CreateUserRequest request)
        {
            const string DefaultRoleName = "User";

            try
            {
                await _userRules.CheckIfEmailExistsWhenCreating(request.Email);

                if (!string.IsNullOrEmpty(request.PhoneNumber))
                {
                    await _userRules.CheckIfPhoneNumberExistsWhenCreating(request.PhoneNumber);
                }

                var newUser = _mapper.Map<User>(request);

                var result = await _userManager.CreateAsync(newUser, request.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(x => x.Description));
                    throw new BusinessException($"Kullanıcı oluşturulurken kimlik hatası: {errors}", HttpStatusCode.BadRequest);
                }

                var roleResult = await _userManager.AddToRoleAsync(newUser, DefaultRoleName);

                if (!roleResult.Succeeded)
                {
                    var roleErrors = string.Join(", ", roleResult.Errors.Select(x => x.Description));
                    throw new BusinessException($"Kullanıcıya varsayılan rol: '{DefaultRoleName}' atanamadı. Hata: {roleErrors}", HttpStatusCode.InternalServerError);
                }

                var userAsDto = _mapper.Map<UserResponse>(newUser);

                return ServiceResult<UserResponse>.SuccessAsCreated(userAsDto, $"api/users/{newUser.Id}");
            }
            catch (BusinessException ex)
            {
                return ServiceResult<UserResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            try
            {
                await _userRules.CheckIfUserExists(id);

                var user = await _userManager.FindByIdAsync(id.ToString());

                var result = await _userManager.DeleteAsync(user!);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new BusinessException($"Kullanıcı silinirken kimlik hatası oluştu: {errors}", HttpStatusCode.InternalServerError);
                }

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<UserResponse>>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            var usersAsDto = _mapper.Map<IEnumerable<UserResponse>>(users);

            return ServiceResult<IEnumerable<UserResponse>>.Success(usersAsDto);
        }

        public async Task<ServiceResult<UserResponse>> GetByIdAsync(int id)
        {
            try
            {
                await _userRules.CheckIfUserExists(id);

                var user = await _unitOfWork.Users.GetByIdAsync(id);
                var userAsDto = _mapper.Map<UserResponse>(user);

                return ServiceResult<UserResponse>.Success(userAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<UserResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<UserResponse>>> GetPagedAllUsersAsync(int pageNumber, int pageSize)
        {
            try
            {
                _userRules.CheckIfPagingParametersAreValid(pageNumber, pageSize);

                var users = await _unitOfWork.Users.GetAllPagedAsync(pageNumber, pageSize);
                var usersAsDto = _mapper.Map<IEnumerable<UserResponse>>(users);

                return ServiceResult<IEnumerable<UserResponse>>.Success(usersAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<UserResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<UserAggregateResponse>> GetUserAggregateAsync(int id)
        {
            try
            {
                await _userRules.CheckIfUserExists(id);

                var user = await _unitOfWork.Users.GetUserAggregateAsync(id);
                var userAsDto = _mapper.Map<UserAggregateResponse>(user);

                return ServiceResult<UserAggregateResponse>.Success(userAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<UserAggregateResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<UserAggregateResponse>>> GetUsersAggregateAsync()
        {
            var users = await _unitOfWork.Users.GetUsersAggregateAsync();
            var usersAsDto = _mapper.Map<IEnumerable<UserAggregateResponse>>(users);

            return ServiceResult<IEnumerable<UserAggregateResponse>>.Success(usersAsDto);
        }

        public async Task<ServiceResult<IEnumerable<UserWithTicketsResponse>>> GetUsersWithTicketsAsync()
        {
            var users = await _unitOfWork.Users.GetUsersWithTicketsAsync();
            var usersAsDto = _mapper.Map<IEnumerable<UserWithTicketsResponse>>(users);

            return ServiceResult<IEnumerable<UserWithTicketsResponse>>.Success(usersAsDto);
        }

        public async Task<ServiceResult<UserWithTicketsResponse>> GetUserWithTicketsAsync(int id)
        {
            try
            {
                await _userRules.CheckIfUserExists(id);

                var user = await _unitOfWork.Users.GetUserWithTicketsAsync(id);
                var userAsDto = _mapper.Map<UserWithTicketsResponse>(user);

                return ServiceResult<UserWithTicketsResponse>.Success(userAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<UserWithTicketsResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> PassiveAsync(int id)
        {
            try
            {
                await _userRules.CheckIfUserExists(id);

                await _unitOfWork.Users.PassiveAsync(id);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> UpdateAsync(UpdateUserRequest request)
        {
            try
            {
                await _userRules.CheckIfUserExists(request.Id);

                if (!string.IsNullOrEmpty(request.PhoneNumber))
                {
                    await _userRules.CheckIfPhoneNumberExistsWhenUpdating(request.Id, request.PhoneNumber);
                }

                var user = await _unitOfWork.Users.GetByIdAsync(request.Id);
                _mapper.Map(request, user);

                var result = await _userManager.UpdateAsync(user!);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new BusinessException($"Kullanıcı güncellenirken kimlik hatası: {errors}", HttpStatusCode.InternalServerError);
                }

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> UpdateEmailAsync(UpdateUserEmailRequest request)
        {
            try
            {
                await _userRules.CheckIfUserExists(request.Id);
                await _userRules.CheckIfEmailExistsWhenUpdating(request.Id, request.NewEmail);
                await _userRules.CheckIfCurrentPasswordIsValid(request.Id, request.CurrentPassword);

                var user = await _userManager.FindByIdAsync(request.Id.ToString());

                var token = await _userManager.GenerateChangeEmailTokenAsync(user!, request.NewEmail);

                var result = await _userManager.ChangeEmailAsync(user!, request.NewEmail, token);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new BusinessException($"E-posta güncellenirken kimlik hatası: {errors}", HttpStatusCode.BadRequest);
                }

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> UpdatePasswordAsync(UpdateUserPasswordRequest request)
        {
            try
            {
                await _userRules.CheckIfUserExists(request.Id);

                _userRules.CheckIfNewPasswordIsDifferent(request.CurrentPassword, request.NewPassword);

                await _userRules.CheckIfCurrentPasswordIsValid(request.Id, request.CurrentPassword);

                var user = await _userManager.FindByIdAsync(request.Id.ToString());

                var result = await _userManager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new BusinessException($"Şifre güncellenirken kimlik hatası: {errors}", HttpStatusCode.BadRequest);
                }

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }
    }
}