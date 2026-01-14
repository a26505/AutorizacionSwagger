using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RestauranteAPI.Models;
using RestauranteAPI.Repositories;
using RestauranteAPI.DTOs;

namespace RestauranteAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public string Login(LoginDtoIn loginDtoIn)
        {
            var usuarioTask = _usuarioRepository.GetByEmailAsync(loginDtoIn.Email);
            usuarioTask.Wait();
            var usuario = usuarioTask.Result;

            if (usuario == null || usuario.Password != loginDtoIn.Password)
            {
                throw new KeyNotFoundException("Credenciales incorrectas.");
            }

            return GenerateToken(usuario);
        }

        public string Register(UserDtoIn userDtoIn)
        {
            var existingTask = _usuarioRepository.GetByEmailAsync(userDtoIn.Email);
            existingTask.Wait();
            var existing = existingTask.Result;

            if (existing != null)
                throw new Exception("Ya existe un usuario con ese email.");

            var nuevo = new Usuario
            {
                Nombre = userDtoIn.Nombre,
                Email = userDtoIn.Email,
                Password = userDtoIn.Password,
                Rol = string.IsNullOrWhiteSpace(userDtoIn.Rol) ? Roles.User : userDtoIn.Rol
            };

            var addTask = _usuarioRepository.AddAsync(nuevo);
            addTask.Wait();

            return GenerateToken(nuevo);
        }

        private string GenerateToken(Usuario usuario)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Role, usuario.Rol),
                new Claim(ClaimTypes.Email, usuario.Email),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
