using BookstoreApplication.DTOs;

namespace BookstoreApplication.Models
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDto data);
        Task Login(LoginDto data);
    }
}
