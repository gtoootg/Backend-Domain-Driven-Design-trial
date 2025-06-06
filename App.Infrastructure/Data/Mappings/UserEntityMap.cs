using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Domain.Model.User;

namespace App.Infrastructure.Data.Mappings;

public class UserEntityMap : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        // Table name
        builder.ToTable("Users");
        
        // Primary Key
        builder.HasKey(u => u.Id);
        
        // Properties
        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();
            
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);
            
        // Indexes
        builder.HasIndex(u => u.Email)
            .IsUnique();
            
        // Relationships
        builder.HasOne(u => u.Country)
            .WithMany(c => c.Users)
            .HasForeignKey(u => u.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
