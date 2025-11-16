using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            try
            {
                await _userRules.CheckIfUserExists(id);
                await _userRules.CheckIfUserHasNoTickets(id);

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
            try
            {
                var users = await _unitOfWork.Users.GetAllAsync();
                var usersAsDto = _mapper.Map<IEnumerable<UserResponse>>(users);

                return ServiceResult<IEnumerable<UserResponse>>.Success(usersAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<UserResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
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
            try
            {
                var users = await _unitOfWork.Users.GetUsersAggregateAsync();
                var usersAsDto = _mapper.Map<IEnumerable<UserAggregateResponse>>(users);

                return ServiceResult<IEnumerable<UserAggregateResponse>>.Success(usersAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<UserAggregateResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<IEnumerable<UserAdminResponse>>> GetUsersForAdminAsync()
        {
            try
            {
                var users = (await _unitOfWork.Users.GetAllAsync()).ToList();

                var usersAsDto = _mapper.Map<List<UserAdminResponse>>(users);

                for (int i = 0; i < users.Count; i++)
                {
                    var userEntity = users[i];

                    var roles = await _userManager.GetRolesAsync(userEntity);

                    usersAsDto[i].Roles = roles;
                }

                return ServiceResult<IEnumerable<UserAdminResponse>>.Success(usersAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<UserAdminResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
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

        public async Task<ServiceResult> UpdateRoleAsync(UpdateUserRoleRequest request)
        {
            try
            {
                await _userRules.CheckIfUserExists(request.Id);
                await _userRules.CheckIfRoleExists(request.NewRoleName);

                var user = await _userManager.FindByIdAsync(request.Id.ToString());

                var currentRoles = await _userManager.GetRolesAsync(user!);

                var removeResult = await _userManager.RemoveFromRolesAsync(user!, currentRoles);
                if (!removeResult.Succeeded)
                {
                    var errors = string.Join(", ", removeResult.Errors.Select(e => e.Description));
                    throw new BusinessException($"Kullanıcının mevcut rolleri kaldırılırken hata oluştu: {errors}", HttpStatusCode.InternalServerError);
                }

                var addResult = await _userManager.AddToRoleAsync(user!, request.NewRoleName);

                if (!addResult.Succeeded)
                {
                    var errors = string.Join(", ", addResult.Errors.Select(e => e.Description));
                    throw new BusinessException($"Kullanıcıya yeni rol ('{request.NewRoleName}') atanırken hata oluştu: {errors}", HttpStatusCode.InternalServerError);
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