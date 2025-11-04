using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NexusTix.Application.Common.Security;
using NexusTix.Application.Features.Auth.Requests;
using NexusTix.Application.Features.Auth.Responses;
using NexusTix.Application.Features.Auth.Rules;
using NexusTix.Application.Features.Users.Responses;
using NexusTix.Domain.Entities;
using NexusTix.Domain.Exceptions;
using System.Net;

namespace NexusTix.Application.Features.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IAuthBusinessRules _authRules;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthService(IMapper mapper, IAuthBusinessRules authRules, UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _authRules = authRules;
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        public async Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new BusinessException("E-posta veya şifre hatalı.", HttpStatusCode.Unauthorized);
                }

                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);
                if (!signInResult.Succeeded)
                {
                    throw new BusinessException("E-posta veya şifre hatalı.", HttpStatusCode.Unauthorized);
                }

                var roles = await _userManager.GetRolesAsync(user);

                (string token, DateTime expiration) = _tokenService.GenerateToken(user, roles);

                var response = new LoginResponse(
                    Token: token,
                    Expiration: expiration,
                    Email: user.Email!,
                    FirstName: user.FirstName,
                    LastName: user.LastName
                    );

                return ServiceResult<LoginResponse>.Success(response);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<LoginResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<UserResponse>> RegisterAsync(CreateUserRequest request)
        {
            const string DefaultRoleName = "User";

            try
            {
                await _authRules.CheckIfEmailExistsWhenCreating(request.Email);

                if (!string.IsNullOrEmpty(request.PhoneNumber))
                {
                    await _authRules.CheckIfPhoneNumberExistsWhenCreating(request.PhoneNumber);
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

                return ServiceResult<UserResponse>.SuccessAsCreated(userAsDto, $"api/auth/{newUser.Id}");
            }
            catch (BusinessException ex)
            {
                return ServiceResult<UserResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> UpdateEmailAsync(UpdateUserEmailRequest request)
        {
            try
            {
                await _authRules.CheckIfUserExists(request.Id);
                await _authRules.CheckIfEmailExistsWhenUpdating(request.Id, request.NewEmail);
                await _authRules.CheckIfCurrentPasswordIsValid(request.Id, request.CurrentPassword);

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
                await _authRules.CheckIfUserExists(request.Id);
                _authRules.CheckIfNewPasswordIsDifferent(request.CurrentPassword, request.NewPassword);
                await _authRules.CheckIfCurrentPasswordIsValid(request.Id, request.CurrentPassword);

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
