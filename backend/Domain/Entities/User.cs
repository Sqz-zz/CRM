using CRM.Domain.Common;
using CRM.Domain.Enums;
using CRM.Domain.Entities;

namespace CRM.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }

        // Поля для безопасности 
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        // Навигация
        public ICollection<Note> CreatedNotes { get; set; } = new List<Note>();
    }
}