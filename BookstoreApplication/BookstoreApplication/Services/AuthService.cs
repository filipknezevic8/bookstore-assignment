using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BookstoreApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
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
            await _userManager.AddToRoleAsync(user, "Librarian");
        }

        public async Task<string> Login(LoginDto data)
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
            var token = await GenerateJwt(user);
            return token;
        }

        private async Task<string> GenerateJwt(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.Id),  // 'sub' atribut
              new Claim("username", user.UserName),  // 'username' atribut
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  // jedinstveni identifikator tokena
            };

            // Konfiguracija za generisanje tokena
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim("role", role)));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
              issuer: _configuration["Jwt:Issuer"],
              audience: _configuration["Jwt:Audience"],
              claims: claims,
              expires: DateTime.UtcNow.AddDays(1), // važi 1 dan ('exp' atribut), nakon čega mora nova prijava
              signingCredentials: creds
            );

            // Generisanje tokena
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ProfileDto> GetProfile(ClaimsPrincipal userPrincipal)
        {
            // Preuzimanje korisničkog imena iz tokena
            var username = userPrincipal.FindFirstValue("username");

            if (username == null)
            {
                throw new BadRequestException("Token is invalid");
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new NotFoundException("User with provided username does not exist");
            }

            return _mapper.Map<ProfileDto>(user);
        }

        public async Task<string> LoginWithGoogle(string email, string name, string surname)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                // Ako korisnik ne postoji u bazi, može se kreirati
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    Name = name,
                    Surname = surname
                };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Librarian"); // default role
                }
                else
                {
                    throw new BadRequestException("Google login failed: user creation error");
                }
            }

            // Ponovo se koristi GenerateJwt, isto kao za običan login
            var token = await GenerateJwt(user);
            return token;
        }
    }
}
