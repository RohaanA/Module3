using Application.Services.Authentication;
using Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _auth;

        public AuthenticationController(IAuthenticationService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = _auth.Register(request.FirstName, request.LastName, request.Email, request.Password, request.Role);

            var response = new AuthResponse(
                authResult.Id,
                authResult.FirstName,
                authResult.LastName,
                authResult.Email,
                authResult.Token
                );
            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login (LoginRequest request)
        {
            var authResult = await _auth.Login(request.Email, request.Password);

            var response = new AuthResponse(
                authResult.Id,
                authResult.FirstName,
                authResult.LastName,
                authResult.Email,
                authResult.Token
                );
            return Ok(response);
        }
    }
}
