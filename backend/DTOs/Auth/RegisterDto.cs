using CRM.Domain.Enums;
namespace CRM.DTOs.Auth
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}