using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationService.Domain.Entities;

namespace RegistrationService.Infrastructure.Persistence.Configurations;

public sealed class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
{
    public void Configure(EntityTypeBuilder<Registration> builder)
    {
        builder.ToTable("Registrations");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.RegistrationNumber).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Notes).HasMaxLength(500);
        builder.HasIndex(x => x.RegistrationNumber).IsUnique();
        builder.Property(x => x.RegisteredAtUtc).IsRequired();
    }
}
