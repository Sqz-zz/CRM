
using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistence.Configuration
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(s => s.Title)
                .IsUnique();

            builder.Property(s => s.OrderIndex)
                .HasDefaultValue(0);

            // Связь Status -> Clients
            builder.HasMany(s => s.Clients)
                .WithOne(c => c.Status)
                .HasForeignKey(c => c.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
            // RESTRICT
            // Сначала нужно перевести клиентов в другой статус.
        }
    }
}