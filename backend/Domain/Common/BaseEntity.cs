using System;

namespace CRM.Domain.Common
{
    // Абстрактный класс,  Все сущности будут наследоваться от него
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}