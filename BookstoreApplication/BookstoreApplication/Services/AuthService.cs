using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace BookstoreApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task RegisterAsync(RegistrationDto data)
        {
            var user = _mapper.Map<ApplicationUser>(data);
            var result = await _userManager.CreateAsync(user, data.Password);
            if (!result.Succeeded)
            {
                // više grešaka se može desiti pri registraciji
                // preuzima se tekstualni opis svake i spaja u jedan string
                string errorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new BadRequestException(errorMessage);
            }
        }

        public async Task Login(LoginDto data)
        {
            // pronalaženje korisnika prema korisničkom imenu
            var user = await _userManager.FindByNameAsync(data.Username);
            if (user == null)
            {
                throw new BadRequestException("Invalid credentials.");
            }

            // provera da li lozinka odgovara nađenom korisniku
            var passwordMatch = await _userManager.CheckPasswordAsync(user, data.Password);
            if (!passwordMatch)
            {
                throw new BadRequestException("Invalid credentials.");
            }
        }
    }
}
