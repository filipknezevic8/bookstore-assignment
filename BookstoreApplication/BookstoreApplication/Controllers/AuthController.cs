using System.Security.Claims;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationDto data)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return BadRequest("Already logged in.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _authService.RegisterAsync(data);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto data)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return BadRequest("Already logged in.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _authService.Login(data));
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            return Ok(await _authService.GetProfile(User));
        }

        // ova metoda pokreće prijavu preko Google-a
        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = "/api/Auth/google-response" };
            return Challenge(properties, "Google");
        }

        // ova metoda obrađuje odgovor od Google-a, kreira JWT i preusmerava korisnika na frontend
        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync("Google");

            if (!result.Succeeded)
                return Unauthorized();

            var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var name = result.Principal.FindFirstValue(ClaimTypes.GivenName);
            var surname = result.Principal.FindFirstValue(ClaimTypes.Surname);

            if (email == null)
                return BadRequest("Email not found in Google login response.");

            // Login ili kreiranje korisnika
            var token = await _authService.LoginWithGoogle(email, name, surname);

            // Preusmeravanje na frontend sa JWT u query param
            var frontendUrl = $"http://localhost:5173/google-callback?token={token}";
            return Redirect(frontendUrl);
        }
    }
}
