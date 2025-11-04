using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusTix.Application.Features.Auth;
using NexusTix.Application.Features.Auth.Requests;
using NexusTix.Domain.Exceptions;
using System.Net;
using System.Security.Claims;

namespace NexusTix.WebAPI.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            return CreateActionResult(await _authService.RegisterAsync(request));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return CreateActionResult(await _authService.LoginAsync(request));
        }

        [HttpPut("update-email")]
        [Authorize]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateUserEmailRequest request)
        {
            var authenticatedUserId = GetAuthenticatedUserId();
            if (authenticatedUserId != request.Id)
            {
                return Unauthorized();
            }

            return CreateActionResult(await _authService.UpdateEmailAsync(request));
        }

        [HttpPut("update-password")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserPasswordRequest request)
        {
            var authenticatedUserId = GetAuthenticatedUserId();
            if (authenticatedUserId != request.Id)
            {
                return Unauthorized();
            }

            return CreateActionResult(await _authService.UpdatePasswordAsync(request));
        }

        [NonAction]
        private int GetAuthenticatedUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                throw new BusinessException("Kullanıcı kimliği doğrulanamadı.", HttpStatusCode.Unauthorized);
            }

            return int.Parse(userIdString);
        }
    }
}
