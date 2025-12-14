using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.DTOs.Auth;
using CRM.Infrastructure.Jwt;
using CRM.Infrastructure.Persistence;
using CRM.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtTokenGenerator _jwt;

        public AuthService(
            ApplicationDbContext context,
            JwtTokenGenerator jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var exists = await _context.Users
                .AnyAsync(u => u.Username == dto.Username);

            if (exists)
                throw new Exception("Username already taken");

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = Role.User
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = _jwt.Generate(user),
                Role = user.Role.ToString()
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null)
                throw new Exception("Invalid credentials");

            var valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!valid)
                throw new Exception("Invalid credentials");

            return new AuthResponseDto
            {
                Token = _jwt.Generate(user),
                Role = user.Role.ToString()
            };
        }
    }
}