using System.Security.Claims;
using BookstoreApplication.DTOs;

namespace BookstoreApplication.Models
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDto data);
        Task<string> Login(LoginDto data);
        Task<ProfileDto> GetProfile(ClaimsPrincipal userPrincipal);
        public Task<string> LoginWithGoogle(string email, string? name, string? surname);
    }
}
